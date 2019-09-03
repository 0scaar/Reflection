using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            var controllerNomeCompleto = $"ByteBank.Portal.Controller.{controllerNome}Controller";

            //var controller = Activator.CreateInstance(typeof("ByteBank.Portal"), controllerNomeCompleto, new object[0]); 
        }
    }
}
