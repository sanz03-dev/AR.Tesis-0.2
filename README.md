# 🧠 Aplicación de Realidad Aumentada para el Análisis de la Percepción Espacial

## 📌 Descripción

Este proyecto corresponde al desarrollo de una aplicación móvil para Android basada en tecnologías de **Realidad Aumentada (RA)**, orientada al análisis de la percepción espacial mediante la interacción con figuras tridimensionales ambiguas.

La aplicación permite a los usuarios visualizar, manipular y analizar objetos 3D como el Cubo de Necker o la Escalera de Penrose, integrando además módulos de evaluación cognitiva y contenido educativo.

---

## 🎯 Objetivo

Proporcionar una herramienta interactiva que contribuya al estudio de los mecanismos perceptivos, combinando visualización en RA, evaluación mediante quizzes y retroalimentación del desempeño del usuario.

---

## 🧩 Funcionalidades principales

* 📱 Visualización de objetos 3D en RA
* ✋ Interacción táctil (rotación, desplazamiento y escalado)
* 🧪 Sistema de quizzes (modo percepción y conocimiento)
* 📊 Retroalimentación de resultados
* 📚 Módulo informativo sobre figuras ambiguas
* 🔊 Integración de audio (música y efectos)

---

## 🛠️ Tecnologías utilizadas

* **Unity** (Motor de desarrollo)
* **AR Foundation + ARCore** (Realidad Aumentada)
* **C#** (Lógica del sistema)
* **Blender** (Modelado 3D)
* **JSON** (Gestión de datos)

---

## 📁 Estructura del proyecto

A continuación se muestra la organización principal del proyecto dentro de Unity:

```
Assets/
│
├── Scenes/                # Escenas principales (MainMenu, AR, Info, Quiz)
│
├── Scripts/               # ⭐ LÓGICA PRINCIPAL DE LA APLICACIÓN
│   ├── AR/                # Interacción en Realidad Aumentada
│   ├── Quiz/              # Sistema de evaluación
│   ├── Info/              # Control de información
│   
│
├── Prefabs/               # Objetos reutilizables (modelos 3D, UI)
│
├── Materials/             # Materiales de los objetos
│
├── Models/                # Modelos 3D (figuras ambiguas)
│
├── Resources/             # Archivos cargados dinámicamente (JSON, imágenes)
│
├── Audio/                 # Música y efectos de sonido
│
└── UI/                    # Elementos visuales (botones, paneles, layouts)
```

🔴 **Importante:**
Toda la lógica del sistema está implementada en la carpeta:

👉 `Assets/Scripts/`

---

## 🧠 Arquitectura

El sistema sigue una arquitectura modular basada en gestores (Managers), donde:

* **QuizManager** → Control del flujo del sistema de evaluación
* **ARManager** → Gestión de la experiencia en RA
* **InfoManager** → Navegación del módulo informativo

---

## 🚀 Estado del proyecto

✔ Funcional
✔ Integración completa de módulos
✔ Preparado para pruebas en dispositivo móvil

---

## 📌 Autor

Proyecto desarrollado como parte de trabajo de tesis universitaria en el área de tecnolías interactivas y percepción espacial.

---

## 📄 Licencia

Uso académico.
