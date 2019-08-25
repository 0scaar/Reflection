﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));

            _prefixos = prefixos;
        }

        public void Iniciar()
        {
            while (true)
                ManipularRequisicao();
        }

        private void ManipularRequisicao()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var path = requisicao.Url.AbsolutePath;
            if (path == "/Assets/css/styles.css")
            {
                // retorna o documento style.css
                var assembly = Assembly.GetExecutingAssembly();
                var nomeResourve = "ByteBank.Portal.Assets.css.styles.css";

                var resourceStream = assembly.GetManifestResourceStream(nomeResourve);
                var bytesResource = new byte[resourceStream.Length];

                resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                resposta.ContentType = "text/css; charset=utf-8";
                resposta.StatusCode = 200;
                resposta.ContentLength64 = resourceStream.Length;

                resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                resposta.OutputStream.Close();
            }
            else if(path == "/Assets/js/main.js")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var nomeResourve = "ByteBank.Portal.Assets.js.main.js";

                var resourceStream = assembly.GetManifestResourceStream(nomeResourve);
                var bytesResource = new byte[resourceStream.Length];

                resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                resposta.ContentType = "application/js; charset=utf-8";
                resposta.StatusCode = 200;
                resposta.ContentLength64 = resourceStream.Length;

                resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                resposta.OutputStream.Close();
            }

            httpListener.Stop();
        }
    }
}
