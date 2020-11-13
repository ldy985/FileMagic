using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Abstracts.Enums;
using ldy985.FileMagic.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ldy985.FileMagic
{
    public class FileMagic : IDisposable, IFileMagic
    {
        private readonly ILogger<FileMagic> _logger;
        private readonly IRuleProvider _ruleProvider;
        private readonly ServiceProvider _provider;
        private readonly IOptions<FileMagicConfig> _config;
        private readonly IParallelMagicMatcher _parallelMagicMatcher;
        private readonly IParsedHandlerProvider _handlerProvider;

        public FileMagic(IOptions<FileMagicConfig> config)
        {
            _config = config;
            ServiceCollection services = new ServiceCollection();
            services.AddFileMagic();
            _provider = services.BuildServiceProvider();

            _logger = _provider.GetRequiredService<ILogger<FileMagic>>();
            _ruleProvider = _provider.GetRequiredService<IRuleProvider>();
            _parallelMagicMatcher = _provider.GetRequiredService<IParallelMagicMatcher>();
            if (_config.Value.ParserHandle)
                _handlerProvider = _provider.GetRequiredService<IParsedHandlerProvider>();
        }

        public FileMagic(ILogger<FileMagic> logger, IRuleProvider ruleProvider, IParallelMagicMatcher parallelMagicMatcher, IParsedHandlerProvider handlerProvider, IOptions<FileMagicConfig> config)
        {
            _logger = logger;
            _ruleProvider = ruleProvider;
            _parallelMagicMatcher = parallelMagicMatcher;
            _handlerProvider = handlerProvider;
            _config = config;
        }

        /// <summary>
        /// IdentifyStream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="result"></param>
        /// <param name="helpingData"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public bool IdentifyStream(Stream stream, out IResult result, ref IMetaData helpingData)
        {
            result = new Result();

            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                string name = _parallelMagicMatcher.GetType().Name;
                _logger.LogTrace("Trying {Matcher} matcher", name);
                if (_parallelMagicMatcher.TryFind(binaryReader, helpingData, out IEnumerable<IRule>? matchedRules))
                {
                    _logger.LogDebug("{Matcher} matched:", name);

#if NETSTANDARD2_1
                    foreach (IRule rule in matchedRules)
#else
                    foreach (IRule rule in matchedRules!)
#endif
                    {
                        _logger.LogDebug("Rule: {Rule}", rule.Name);

                        (bool _, bool structureMatched, bool parserMatched) = RuleMatches(binaryReader, rule, result, false, _config.Value.StructureCheck, _config.Value.ParserCheck);

                        if (!structureMatched && !parserMatched && (rule.HasStructure || rule.HasParser))
                            continue;

                        RuleHelper.AddData(result, rule);

                        return true;
                    }
                }
                else
                {
                    foreach (IRule rule in _ruleProvider.ComplexRulesOnly)
                    {
                        (bool _, bool structureMatched, bool parserMatched) = RuleMatches(binaryReader, rule, result, false, _config.Value.StructureCheck, _config.Value.ParserCheck);

                        if (rule.HasStructure && rule.HasParser && structureMatched && parserMatched)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }

                        if (rule.HasParser && parserMatched && !rule.HasStructure)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }

                        if (rule.HasStructure && structureMatched && !rule.HasParser)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool IdentifyStream(Stream stream, out IResult result)
        {
            result = new Result();

            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                string name = _parallelMagicMatcher.GetType().Name;
                _logger.LogTrace("Trying {Matcher} matcher", name);
                if (_parallelMagicMatcher.TryFind(binaryReader, out IEnumerable<IRule>? matchedRules))
                {
                    _logger.LogDebug("{Matcher} matched", name);
#if NETSTANDARD2_1
                    foreach (IRule rule in matchedRules)
#else
                    foreach (IRule rule in matchedRules!)
#endif
                    {
                        (bool _, bool structureMatched, bool parserMatched) = RuleMatches(binaryReader, rule, result, false, _config.Value.StructureCheck, _config.Value.ParserCheck);

                        if (rule.HasStructure && !structureMatched || rule.HasParser && !parserMatched)
                            return false;

                        RuleHelper.AddData(result, rule);
                        return true;
                    }
                }
                else
                {
                    foreach (IRule rule in _ruleProvider.ComplexRulesOnly)
                    {
                        (bool _, bool structureMatched, bool parserMatched) = RuleMatches(binaryReader, rule, result, false, _config.Value.StructureCheck, _config.Value.ParserCheck);

                        if (rule.HasStructure && rule.HasParser && structureMatched && parserMatched)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }

                        if (rule.HasParser && parserMatched && !rule.HasStructure)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }

                        if (rule.HasStructure && structureMatched && !rule.HasParser)
                        {
                            RuleHelper.AddData(result, rule);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        public bool StreamMatches<T>(Stream stream, out IResult result) where T : IRule
        {
            T rule = _ruleProvider.Get<T>();
            result = new Result();

            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                (bool patternMatched, bool structureMatched, bool parserMatched) = RuleMatches(binaryReader, rule, result, _config.Value.PatternCheck, _config.Value.StructureCheck, _config.Value.ParserCheck);

                if (!patternMatched && !structureMatched && !parserMatched)
                    return false;

                RuleHelper.AddData(result, rule);
                return true;
            }
        }

        /// <summary>
        /// StructureMatched
        /// </summary>
        /// <param name="binaryReader"></param>
        /// <param name="rule"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="IOException">Ignore.</exception>
        private (bool patternMatched, bool structureMatched, bool parserMatched) RuleMatches(BinaryReader binaryReader, IRule rule, IResult result, bool patternCheck, bool structureCheck, bool parserCheck)
        {
            bool patternMatched = false;
            bool structureMatched = false;
            bool parserMatched = false;
            if (patternCheck && rule.HasMagic)
            {
                _logger.LogTrace("Testing {Rule} pattern", rule.Name);
                if (rule.TryMagic(binaryReader))
                {
                    _logger.LogDebug("Matched {Rule} pattern", rule.Name);
                    patternMatched = true;
                    result.MatchedRuleTypes |= MatchTypes.Signature;
                }
            }

            if (structureCheck && rule.HasStructure)
            {
                _logger.LogTrace("Testing {Rule} structure", rule.Name);
                if (rule.TryStructure(binaryReader, result))
                {
                    _logger.LogDebug("Matched {Rule} structure", rule.Name);
                    structureMatched = true;
                    result.MatchedRuleTypes |= MatchTypes.Structure;
                }
            }

            if (parserCheck && rule.HasParser)
            {
                _logger.LogTrace("Testing {Rule} parser", rule.Name);
                if (rule.TryParse(binaryReader, result, out var parsedObject))
                {
                    if (_config.Value.ParserHandle)
#if NETSTANDARD2_1
                        _handlerProvider.ExecuteHandlers(rule, parsedObject);
#else
                        _handlerProvider.ExecuteHandlers(rule, parsedObject!);
#endif

                    _logger.LogDebug("Matched {Rule} parser", rule.Name);
                    parserMatched = true;
                    result.MatchedRuleTypes |= MatchTypes.Parser;
                }
            }

            return (patternCheck && rule.HasMagic && patternMatched, structureCheck && rule.HasStructure && structureMatched, parserCheck && rule.HasParser && parserMatched);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _provider?.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        ~FileMagic()
        {
            Dispose(false);
        }
    }
}