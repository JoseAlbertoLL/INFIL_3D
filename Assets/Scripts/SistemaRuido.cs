using System;
using UnityEngine;

public static class SistemaRuido
{
    public static event Action<Vector3, float> RuidoGenerado;

    public static void EmitirRuido(Vector3 posicion, float radio)
    {
        RuidoGenerado?.Invoke(posicion, radio);
    }
}
