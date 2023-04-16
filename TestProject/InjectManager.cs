using System;
using System.IO;

namespace TestProject
{
    public class InjectManager
    {
        private readonly InjectProvider _injectProvider;
        
        public InjectManager(InjectProvider injectProvider)
        {
            _injectProvider = injectProvider;
            Injected();
        }

        private void Injected()
        {
            _injectProvider.Injected();
        }

        public bool Test(string message)
        {
            if (message != String.Empty)
                return true;
            return false;
        }
        
    }
}