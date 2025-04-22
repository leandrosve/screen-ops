import { OptionBase } from "chakra-react-select";

export interface LanguageOption extends OptionBase {
    label: string;
    value: string;
  }
  
const languageOptions:LanguageOption[] = [
  {
    value: "aa",
    label: "Afar",
  },
  {
    value: "ab",
    label: "Abjasio",
  },
  {
    value: "ae",
    label: "Avéstico",
  },
  {
    value: "af",
    label: "Afrikáans",
  },
  {
    value: "ak",
    label: "Akan",
  },
  {
    value: "am",
    label: "Amárico",
  },
  {
    value: "an",
    label: "Aragonés",
  },
  {
    value: "ar",
    label: "Árabe",
  },
  {
    value: "as",
    label: "Asamés",
  },
  {
    value: "av",
    label: "Avar",
  },
  {
    value: "ay",
    label: "Aimara",
  },
  {
    value: "az",
    label: "Azerí",
  },
  {
    value: "ba",
    label: "Bashkir",
  },
  {
    value: "be",
    label: "Bielorruso",
  },
  {
    value: "bg",
    label: "Búlgaro",
  },
  {
    value: "bh",
    label: "Lenguas bihari",
  },
  {
    value: "bi",
    label: "Bislama",
  },
  {
    value: "bm",
    label: "Bambara",
  },
  {
    value: "bn",
    label: "Bengalí",
  },
  {
    value: "bo",
    label: "Tibetano",
  },
  {
    value: "br",
    label: "Bretón",
  },
  {
    value: "bs",
    label: "Bosnio",
  },
  {
    value: "ca",
    label: "Catalán",
  },
  {
    value: "ce",
    label: "Checheno",
  },
  {
    value: "ch",
    label: "Chamorro",
  },
  {
    value: "co",
    label: "Corso",
  },
  {
    value: "cr",
    label: "Cree",
  },
  {
    value: "cs",
    label: "Checo",
  },
  {
    value: "cu",
    label: "Eslavo eclesiástico",
  },
  {
    value: "cv",
    label: "Chuvasio",
  },
  {
    value: "cy",
    label: "Galés",
  },
  {
    value: "da",
    label: "Danés",
  },
  {
    value: "de",
    label: "Alemán",
  },
  {
    value: "dv",
    label: "Maldivo",
  },
  {
    value: "dz",
    label: "Dzongkha",
  },
  {
    value: "ee",
    label: "Ewe",
  },
  {
    value: "el",
    label: "Griego",
  },
  {
    value: "en",
    label: "Inglés",
  },
  {
    value: "eo",
    label: "Esperanto",
  },
  {
    value: "es",
    label: "Español",
  },
  {
    value: "et",
    label: "Estonio",
  },
  {
    value: "eu",
    label: "Vasco",
  },
  {
    value: "fa",
    label: "Persa",
  },
  {
    value: "ff",
    label: "Fula",
  },
  {
    value: "fi",
    label: "Finés",
  },
  {
    value: "fj",
    label: "Fiyiano",
  },
  {
    value: "fo",
    label: "Feroés",
  },
  {
    value: "fr",
    label: "Francés",
  },
  {
    value: "fy",
    label: "Frisón occidental",
  },
  {
    value: "ga",
    label: "Irlandés",
  },
  {
    value: "gd",
    label: "Gaélico",
  },
  {
    value: "gl",
    label: "Gallego",
  },
  {
    value: "gn",
    label: "Guaraní",
  },
  {
    value: "gu",
    label: "Guyaratí",
  },
  {
    value: "gv",
    label: "Manés",
  },
  {
    value: "ha",
    label: "Hausa",
  },
  {
    value: "he",
    label: "Hebreo",
  },
  {
    value: "hi",
    label: "Hindi",
  },
  {
    value: "ho",
    label: "Hiri motu",
  },
  {
    value: "hr",
    label: "Croata",
  },
  {
    value: "ht",
    label: "Haitiano",
  },
  {
    value: "hu",
    label: "Húngaro",
  },
  {
    value: "hy",
    label: "Armenio",
  },
  {
    value: "hz",
    label: "Herero",
  },
  {
    value: "ia",
    label: "Interlingua",
  },
  {
    value: "id",
    label: "Indonesio",
  },
  {
    value: "ie",
    label: "Occidental",
  },
  {
    value: "ig",
    label: "Igbo",
  },
  {
    value: "ii",
    label: "Yi de Sichuan",
  },
  {
    value: "ik",
    label: "Inupiaq",
  },
  {
    value: "io",
    label: "Ido",
  },
  {
    value: "is",
    label: "Islandés",
  },
  {
    value: "it",
    label: "Italiano",
  },
  {
    value: "iu",
    label: "Inuktitut",
  },
  {
    value: "ja",
    label: "Japonés",
  },
  {
    value: "jv",
    label: "Javanés",
  },
  {
    value: "ka",
    label: "Georgiano",
  },
  {
    value: "kg",
    label: "Kongo",
  },
  {
    value: "ki",
    label: "Kikuyu",
  },
  {
    value: "kj",
    label: "Kuanyama",
  },
  {
    value: "kk",
    label: "Kazajo",
  },
  {
    value: "kl",
    label: "Groenlandés",
  },
  {
    value: "km",
    label: "Jemer central",
  },
  {
    value: "kn",
    label: "Canarés",
  },
  {
    value: "ko",
    label: "Coreano",
  },
  {
    value: "kr",
    label: "Kanuri",
  },
  {
    value: "ks",
    label: "Cachemiro",
  },
  {
    value: "ku",
    label: "Kurdo",
  },
  {
    value: "kv",
    label: "Komi",
  },
  {
    value: "kw",
    label: "Córnico",
  },
  {
    value: "ky",
    label: "Kirguís",
  },
  {
    value: "la",
    label: "Latín",
  },
  {
    value: "lb",
    label: "Luxemburgués",
  },
  {
    value: "lg",
    label: "Ganda",
  },
  {
    value: "li",
    label: "Limburgués",
  },
  {
    value: "ln",
    label: "Lingala",
  },
  {
    value: "lo",
    label: "Lao",
  },
  {
    value: "lt",
    label: "Lituano",
  },
  {
    value: "lu",
    label: "Luba-katanga",
  },
  {
    value: "lv",
    label: "Letón",
  },
  {
    value: "mg",
    label: "Malgache",
  },
  {
    value: "mh",
    label: "Marshalés",
  },
  {
    value: "mi",
    label: "Maorí",
  },
  {
    value: "mk",
    label: "Macedonio",
  },
  {
    value: "ml",
    label: "Malayalam",
  },
  {
    value: "mn",
    label: "Mongol",
  },
  {
    value: "mr",
    label: "Maratí",
  },
  {
    value: "ms",
    label: "Malayo",
  },
  {
    value: "mt",
    label: "Maltés",
  },
  {
    value: "my",
    label: "Birmano",
  },
  {
    value: "na",
    label: "Nauruano",
  },
  {
    value: "nb",
    label: "Noruego bokmål",
  },
  {
    value: "nd",
    label: "Ndebele del norte",
  },
  {
    value: "ne",
    label: "Nepalí",
  },
  {
    value: "ng",
    label: "Ndonga",
  },
  {
    value: "nl",
    label: "Neerlandés",
  },
  {
    value: "nn",
    label: "Noruego nynorsk",
  },
  {
    value: "no",
    label: "Noruego",
  },
  {
    value: "nr",
    label: "Ndebele del sur",
  },
  {
    value: "nv",
    label: "Navajo",
  },
  {
    value: "ny",
    label: "Chichewa",
  },
  {
    value: "oc",
    label: "Occitano",
  },
  {
    value: "oj",
    label: "Ojibwa",
  },
  {
    value: "om",
    label: "Oromo",
  },
  {
    value: "or",
    label: "Oriya",
  },
  {
    value: "os",
    label: "Osetio",
  },
  {
    value: "pa",
    label: "Panyabí",
  },
  {
    value: "pi",
    label: "Pali",
  },
  {
    value: "pl",
    label: "Polaco",
  },
  {
    value: "ps",
    label: "Pastún",
  },
  {
    value: "pt",
    label: "Portugués",
  },
  {
    value: "qu",
    label: "Quechua",
  },
  {
    value: "rm",
    label: "Romanche",
  },
  {
    value: "rn",
    label: "Kirundi",
  },
  {
    value: "ro",
    label: "Rumano",
  },
  {
    value: "ru",
    label: "Ruso",
  },
  {
    value: "rw",
    label: "Kinyarwanda",
  },
  {
    value: "sa",
    label: "Sánscrito",
  },
  {
    value: "sc",
    label: "Sardo",
  },
  {
    value: "sd",
    label: "Sindhi",
  },
  {
    value: "se",
    label: "Sami del norte",
  },
  {
    value: "sg",
    label: "Sango",
  },
  {
    value: "si",
    label: "Cingalés",
  },
  {
    value: "sk",
    label: "Eslovaco",
  },
  {
    value: "sl",
    label: "Esloveno",
  },
  {
    value: "sm",
    label: "Samoano",
  },
  {
    value: "sn",
    label: "Shona",
  },
  {
    value: "so",
    label: "Somalí",
  },
  {
    value: "sq",
    label: "Albanés",
  },
  {
    value: "sr",
    label: "Serbio",
  },
  {
    value: "ss",
    label: "Suazi",
  },
  {
    value: "st",
    label: "Sesotho",
  },
  {
    value: "su",
    label: "Sundanés",
  },
  {
    value: "sv",
    label: "Sueco",
  },
  {
    value: "sw",
    label: "Suajili",
  },
  {
    value: "ta",
    label: "Tamil",
  },
  {
    value: "te",
    label: "Telugú",
  },
  {
    value: "tg",
    label: "Tayiko",
  },
  {
    value: "th",
    label: "Tailandés",
  },
  {
    value: "ti",
    label: "Tigriña",
  },
  {
    value: "tk",
    label: "Turcomano",
  },
  {
    value: "tl",
    label: "Tagalo",
  },
  {
    value: "tn",
    label: "Setsuana",
  },
  {
    value: "to",
    label: "Tongano",
  },
  {
    value: "tr",
    label: "Turco",
  },
  {
    value: "ts",
    label: "Tsonga",
  },
  {
    value: "tt",
    label: "Tártaro",
  },
  {
    value: "tw",
    label: "Twi",
  },
  {
    value: "ty",
    label: "Tahitiano",
  },
  {
    value: "ug",
    label: "Uigur",
  },
  {
    value: "uk",
    label: "Ucraniano",
  },
  {
    value: "ur",
    label: "Urdu",
  },
  {
    value: "uz",
    label: "Uzbeko",
  },
  {
    value: "ve",
    label: "Venda",
  },
  {
    value: "vi",
    label: "Vietnamita",
  },
  {
    value: "vo",
    label: "Volapük",
  },
  {
    value: "wa",
    label: "Valón",
  },
  {
    value: "wo",
    label: "Wólof",
  },
  {
    value: "xh",
    label: "Xhosa",
  },
  {
    value: "yi",
    label: "Yídish",
  },
  {
    value: "yo",
    label: "Yoruba",
  },
  {
    value: "za",
    label: "Zhuang",
  },
  {
    value: "zh",
    label: "Chino",
  },
  {
    value: "zu",
    label: "Zulú",
  },
];

export default languageOptions;