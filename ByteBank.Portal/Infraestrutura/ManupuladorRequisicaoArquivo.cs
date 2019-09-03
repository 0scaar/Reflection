using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManupuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var nomeResourve = Utilidades.ConverterPathParaNomeAssembly(path);

            var resourceStream = assembly.GetManifestResourceStream(nomeResourve);

            if (resourceStream == null)
            {
                resposta.StatusCode = 404;
                resposta.OutputStream.Close();
            }
            else
            {
                using (resourceStream)
                {
                    var bytesResource = new byte[resourceStream.Length];

                    resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                    resposta.ContentType = Utilidades.BoterTipoDeConteudo(path);
                    resposta.StatusCode = 200;
                    resposta.ContentLength64 = resourceStream.Length;

                    resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                    resposta.OutputStream.Close();
                }

                
            }
        }
    }
}
