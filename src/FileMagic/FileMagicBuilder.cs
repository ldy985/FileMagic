using System;
using ldy985.FileMagic.Abstracts;
using ldy985.FileMagic.Core;
using ldy985.FileMagic.Core.Rules;
using ldy985.FileMagic.Matchers.Signature.Trie;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ldy985.FileMagic
{
    public class FileMagicBuilder
    {
        public static FileMagicBuilder Create(FileMagicConfig options)
        {
            return new FileMagicBuilder(options);
        }

        private readonly ParsedHandlerProvider _parsedHandlerProvider;
        private readonly FileMagicConfig _options;

        private FileMagicBuilder(FileMagicConfig options)
        {
            _options = options;
            _parsedHandlerProvider = new ParsedHandlerProvider();
        }

        public FileMagicBuilder AddParsedHandler<TRule, TParsed>(Action<TParsed> action) where TParsed : IParsed where TRule : IRule
        {
            _parsedHandlerProvider.AddParsedHandler<TRule, TParsed>(action);
            return this;
        }

        public FileMagic Build(ILogger<FileMagic> logger2 = null, ILogger<TrieSignatureMatcher> logger = null)
        {
            RuleProvider ruleProvider = new RuleProvider(FileMagicRuleHelpers.GetDefaultFileMagicRules());

            logger2 ??= NullLogger<FileMagic>.Instance;
            logger ??= NullLogger<TrieSignatureMatcher>.Instance;

            var parallelMagicMatcher = new TrieSignatureMatcher(logger, ruleProvider);

            return new FileMagic(logger2, ruleProvider, parallelMagicMatcher, _parsedHandlerProvider, Options.Create(_options));
        }
    }
}