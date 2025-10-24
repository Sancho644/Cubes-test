using System;
using System.Collections.Generic;

namespace Localization.Config
{
    [Serializable]
    public class LanguageConfig
    {
        public List<LocalizedPhraseConfig> localizedPhrases = new();
    }
}