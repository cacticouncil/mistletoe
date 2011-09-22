using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinMoss
{
    class UnzipInfo
    {
        private String m_zipArchive;

        public String ZipArchive
        {
            get { return m_zipArchive; }
            set { m_zipArchive = value; }
        }
        private String m_destinationFolder;

        public String DestinationFolder
        {
            get { return m_destinationFolder; }
            set { m_destinationFolder = value; }
        }

        private Object m_sender;

        public Object Sender
        {
            get { return m_sender; }
            set { m_sender = value; }
        }

        public UnzipInfo(String _zipArchive, String _destinationFolder, Object _sender)
        {
            this.m_zipArchive = _zipArchive;
            this.m_destinationFolder = _destinationFolder;
            this.m_sender = _sender;
        }
    }
}
