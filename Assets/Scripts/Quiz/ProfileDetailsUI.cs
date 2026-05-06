using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileDetailsUI : MonoBehaviour
{
    public TextMeshProUGUI detailsText;

    public void ShowDetails(string profile)
    {
        Debug.Log("Mostrando perfil: " + profile);

        switch (profile)
        {
           case "Analítico–Secuencial":
           detailsText.text = @"Perfil Analítico–Secuencial (12–24 puntos)

           Las personas con este perfil tienden a abordar las tareas perceptivas de manera analítica y paso a paso, priorizando los detalles individuales sobre la forma global. En actividades cotidianas, esto puede traducirse en una mayor precisión en tareas estructuradas, como el armado de objetos siguiendo instrucciones, la lectura de planos simples o la organización de espacios conocidos.

           Sin embargo, en situaciones que requieren una interpretación rápida de escenas complejas, como orientarse en un entorno nuevo, estimar distancias de manera inmediata o comprender figuras ambiguas, estos individuos pueden experimentar mayor dificultad inicial, ya que necesitan más tiempo para integrar la información visual completa.";
           break;

           case "Mixto o Adaptativo":
           detailsText.text = @"Perfil Mixto o Adaptativo (25–36 puntos)

           Este perfil se caracteriza por una flexibilidad perceptiva, permitiendo alternar entre el análisis detallado y la percepción global según las demandas de la tarea. En la vida cotidiana, estas personas suelen adaptarse con facilidad a actividades que implican orientación espacial, manipulación de objetos tridimensionales o interpretación de imágenes complejas, aunque su desempeño puede mejorar progresivamente con la experiencia.

           La interacción directa con el entorno —como moverse alrededor de un objeto o cambiar el punto de vista— favorece notablemente su comprensión espacial, lo que los hace especialmente receptivos a herramientas interactivas como la Realidad Aumentada.";
           break;

           case "Visual–Espacial":
           detailsText.text = @"Perfil Visual–Espacial (37–48 puntos)

           Las personas con este perfil presentan una alta eficiencia en la percepción visual y espacial, lo que les permite interpretar con facilidad relaciones de profundidad, orientación y estructura tridimensional. En tareas cotidianas, esto se manifiesta en una mayor facilidad para actividades como estacionar un vehículo, interpretar mapas, reorganizar espacios físicos o imaginar cómo encajarán objetos entre sí.

           Además, suelen adaptarse rápidamente a situaciones nuevas que requieren una comprensión espacial inmediata, mostrando menor dependencia de instrucciones explícitas y mayor confianza en la exploración visual del entorno.";
           break;

           case "Intuitivo–Visual Avanzado":
           detailsText.text = @"Perfil Intuitivo–Visual Avanzado (49–60 puntos)

           Este perfil corresponde a individuos con una capacidad perceptiva altamente desarrollada, especialmente en contextos donde existe ambigüedad visual o información incompleta. En la vida diaria, estas personas tienden a desenvolverse con soltura en tareas como la navegación en entornos desconocidos, la interpretación de representaciones espaciales complejas o la manipulación mental de objetos sin necesidad de apoyo visual constante.

           Su desempeño se ve favorecido por la intuición visual y la capacidad de anticipar configuraciones espaciales, lo que les permite tomar decisiones rápidas basadas en la percepción. La Realidad Aumentada potencia aún más estas habilidades al ofrecer múltiples perspectivas y retroalimentación visual en tiempo real.";
           break;
        }
    }

    public void OnClickFinish()
    { 
       AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
       // 1. Apagar este panel de detalles
       this.gameObject.SetActive(false); 

       // 2. Volver al menú principal
       if (QuizManager.instance != null)
       {
          QuizManager.instance.ShowTestMain();
        }
    }
}
