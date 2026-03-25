# 📱 Sistema de Realidad Aumentada para el Análisis de Figuras 3D Ambiguas

## 🎓 Proyecto de Tesis

Aplicación móvil desarrollada en **Unity** que integra técnicas de **Realidad Aumentada (AR)** y evaluación interactiva mediante cuestionarios, orientada al análisis de la percepción espacial a través de figuras geométricas ambiguas.

---

## 🎯 Objetivo del Proyecto

Desarrollar una aplicación Android que permita:

- Visualizar figuras 3D ambiguas.
- Evaluar la percepción espacial mediante cuestionarios interactivos.
- Mostrar información educativa asociada a cada figura.
- Implementar detección de planos mediante AR Foundation.
- Gestionar niveles de dificultad y progreso del usuario.

---

## 🧠 Figuras Implementadas

- Cubo de Necker
- Triángulo de Penrose
- Escalera de Penrose

---

## 🛠 Tecnologías Utilizadas

- Unity
- AR Foundation
- ARCore (Android)
- C#
- TextMeshPro
- JSON para gestión de preguntas
- Git para control de versiones

---

## 🏗 Arquitectura General

El sistema está compuesto por:

- **GameManager (Singleton)**  
  Gestión global del estado del juego.

- **QuizManager**  
  Control del flujo de preguntas y respuestas.

- **SceneLoader**  
  Navegación entre escenas.

- **DifficultySelector / DifficultyUIManager**  
  Gestión de niveles de dificultad.

- **FigureSelector**  
  Selección de figura a analizar.

  **ARManager**
  Gestiona la experiencia de RA
---

## 📂 Estructura del Proyecto
Assets/
│
├── Scripts/ Código del proyecto
├── Scenes/  Escenas
├── Prefabs/ Prefabs
├── Resources/
.
Packages/
ProjectSettings/
.
.

---

## 📦 Funcionalidades Implementadas

- Navegación entre escenas
- Sistema de desbloqueo de dificultad
- Cuestionarios dinámicos cargados desde JSON
- Barra de progreso
- Sistema de retorno y navegación principal
- Experiencia de AR en nivel basico

---

## 🚧 Estado Actual

✔ Sistema de Quiz funcional  
✔ Gestión de dificultad  
✔ Gestión de figuras  
✔ Navegación completa  
🔄 Integración completa de AR en desarrollo  

---

## 📱 Plataforma Objetivo

Android (ARCore compatible)

---

## 👨‍💻 Autor

[Kevin Sánchez Soto]

Proyecto desarrollado como parte del trabajo de tesis universitaria.

---

## 📌 Notas

Este proyecto se encuentra en desarrollo activo y forma parte de una investigación académica en el área de tecnologías inmersivas aplicadas a la educación.

