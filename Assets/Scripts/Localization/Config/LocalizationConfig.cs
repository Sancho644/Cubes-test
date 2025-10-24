using System;

namespace Localization.Config
{
    [Serializable]
    public class LocalizationConfig
    {
        public LanguageConfig languages = new();
    }
}