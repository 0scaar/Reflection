﻿using System;

namespace ByteBank.Service
{
    public interface ICambioService
    {
        decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor);
    }
}
