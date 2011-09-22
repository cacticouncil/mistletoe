using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinMoss
{
    public class LanguageConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Enum.GetNames(typeof(Document.LanguagesEnum)));
        }
    }

    public class Document
    {
        public enum LanguagesEnum
        {
            cpp,
            c,
            csharp,
            java,
            ml,
            pascal,
            ada,
            lisp,
            scheme,
            haskell,
            fortran,
            ascii,
            vhdl,
            perl,
            matlab,
            python,
            mips,
            prolog,
            spice,
            vb,
            modula2,
            a8086,
            javascript,
            plsql,
        }
        static public string[] strarrLanguagesCmdLine = 
        {
            "cc",
            "c",
            "csharp",
            "java",
            "ml",
            "pascal",
            "ada",
            "lisp",
            "scheme",
            "haskell",
            "fortran",
            "ascii",
            "vhdl",
            "perl",
            "matlab",
            "python",
            "mips",
            "prolog",
            "spice",
            "vb",
            "modula2",
            "a8086",
            "javascript",
            "plsql",
        };
        static public string[] strarrLanguagesFilter = 
        {
            "*.cpp *.h *.hpp *.c",
            "*.c .h",
            "*.cs",
            "*.java",
            "*.ml",
            "*.pas",
            "*.ada",
            "*.lisp",
            "*.scm",
            "*.hs",
            "*.f",
            "*.asc",
            "*.vhdl",
            "*.pl",
            "*.m",
            "*.py",
            "*.asm",
            "*.pl",
            "*.cir",
            "*.vb",
            "*.mod",
            "*.asm",
            "*.js",
            "*.sql",
        };

        [CategoryAttribute("Paths"),
        DescriptionAttribute("The location of the Perl Executable."),
        DefaultValueAttribute("Perl.exe"),
        System.ComponentModel.Editor(
            typeof(System.Windows.Forms.Design.FileNameEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        public string PerlLocation
        {
            get { return strPerlLocation; }
            set { strPerlLocation = value; }
        }
        private string strPerlLocation;

        [CategoryAttribute("Paths"),
        DescriptionAttribute("The location of the Moss Perl Script."),
        DefaultValueAttribute("Moss.pl"),
        System.ComponentModel.Editor(
            typeof(System.Windows.Forms.Design.FileNameEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        public string MossScriptLocation
        {
            get { return strMossScriptLocation; }
            set { strMossScriptLocation = value; }
        }
        private string strMossScriptLocation;

        [CategoryAttribute("Command Arguments"),
        DescriptionAttribute("The source language of the programs to be tested."),
        DefaultValueAttribute("cpp"),
        TypeConverter(typeof(LanguageConverter))]
        public string Language
        {
            get { return strLanguage; }
            set { strLanguage = value; }
        }
        private string strLanguage;

        [CategoryAttribute("Command Arguments"),
        DescriptionAttribute("(OPTIONAL) The comment string that is attached to the generated report."),
        DefaultValueAttribute("")]
        public string Comment
        {
            get { return strComment; }
            set { strComment = value; }
        }
        private string strComment;

        [CategoryAttribute("Command Arguments"),
        DescriptionAttribute("The maximum number of times a given passage may appear before it is ignored."),
        DefaultValueAttribute(10)]
        public int IgnoreCount
        {
            get { return numIgnoreCount; }
            set { numIgnoreCount = value; }
        }
        private int numIgnoreCount;
    }

    static class Program
    {
        static public Document theDoc = new Document();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinMoss());
        }
    }
}
