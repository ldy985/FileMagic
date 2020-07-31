//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ldy985.FileMagic;
//using ldy985.FileMagic.Abstracts;
//using ldy985.FileMagic.Core;
//using ldy985.FileMagic.Matchers.Signature.Trie;
//using ldy985.FileMagic.Matchers.Structure.Simple;
//using Microsoft.Extensions.DependencyInjection;
//using Shared;
//using Shared.Interfaces;

//namespace FileIdentifier
//{
//    public class FileGuesserBuilder
//    {
//        private readonly DetectionOptions _options;
//        private FileGuesser _fileGuesser;
//        private readonly List<(Type, Action<object>)> _parsedActions;
//        private IStructureMatcher _structureMatcher;
//        private IByteMatcher _signatureMatcher;
//        private ITextGuesser _textGuesser;

//        public FileGuesserBuilder(DetectionOptions options)
//        {
//            _options = options;
//            _parsedActions = new List<(Type, Action<object>)>();
//        }

//        public FileGuesserBuilder AddDefault()
//        {
           
//            //List<IRule> rules = TypeHelper.CreateInstanceOfAll<IRule>(typeof(BaseRule).Assembly).ToList();

//            //_textGuesser = new Heuristics(_options);
//            _signatureMatcher = new TrieSignatureMatcher(); //rules.Where(rule => rule.Magic != null)
//            _structureMatcher = new SimpleStructureMatcher(); //_options, rules.Where(rule => (rule.HasParser || rule.HasStructure) && rule.Magic == null)
//            return this;
//        }

//        public FileGuesserBuilder AddParsedHandler<TRule, TParsed>(Action<TParsed> action)
//        {
//            _parsedActions.Add((typeof(TRule), obj => action((TParsed)obj)));
//            return this;
//        }

//        public FileGuesser Build()
//        {
//            _fileGuesser = new FileGuesser(_options);
//            _fileGuesser.ByteMatcher = _signatureMatcher;
//            _fileGuesser.StructureMatcher = _structureMatcher;
//            _fileGuesser.TextGuesser = _textGuesser;
//            _fileGuesser.RegisterParsedActions(_parsedActions);
//            return _fileGuesser;
//        }
//    }
//}