# Plataforma Educativa Multimedia Interactiva

Desarrollo de una plataforma web educativa para el desarrollo personal y bienestar basada en multimedia interactiva e hipermedia, donde el alumnado aprende mediante vídeos interactivos, animaciones, audio, cuestionarios y navegación no lineal.

---

## 📋 Prerequisites

- **Node.js** (v18+ recommended) — [Download](https://nodejs.org/)
- **npm** (comes with Node.js)
- **Git** (for version control)
- A modern web browser (Chrome, Firefox, Edge, etc.)

- Backend:
- npm install recorder-js
- Servicios de Azure AI
- dotnet run
---

## 🚀 Getting Started

### 1. Clone/Open the Repository

```bash
cd PlataformaEducativaMultimediaInteractivaEnLaNube
```

### 2. Backend Setup (Express + TypeScript)

Navigate to the BACKEND folder:

```bash
cd BACKEND
npm install
```

Start the development server:

```bash
npm run dev
```

The server runs on `http://localhost:3000` by default. You'll see:
```
Server running on http://localhost:3000
```

### 3. Frontend Setup (Angular)

In a **new terminal**, navigate to the Angular app:

```bash
cd FRONTEND/frontend
npm install
```

Start the Angular development server:

```bash
npm run start
```

Angular runs on `http://localhost:4200` by default. Open a browser and visit `http://localhost:4200`.

---

## 📁 Project Structure

```
PlataformaEducativaMultimediaInteractivaEnLaNube/
├── BACKEND/
│   ├── src/
│   │   └── index.ts           (Express server entry point)
│   ├── tsconfig.json          (TypeScript configuration)
│   ├── package.json
│   └── node_modules/
├── FRONTEND/
│   └── frontend/              (Angular app)
│       ├── src/
│       ├── angular.json
│       ├── package.json
│       └── node_modules/
└── README.md
```

---

## 🛠️ Available Commands

### Backend
- `npm run dev` — Start development server with hot reload (uses ts-node-dev)
- `npm run build` — Compile TypeScript to JavaScript (outputs to `dist/`)
- `npm start` — Start compiled server

### Frontend
- `npm run start` — Start Angular dev server (port 4200)
- `npm run build` — Build for production
- `npm test` — Run unit tests
- `npm run watch` — Watch mode for development

---

## 🔗 Communication

- **Backend API**: `http://localhost:3000`
- **Frontend**: `http://localhost:4200`

To make frontend API calls, install `cors` in the backend or use a proxy configuration in `frontend/src/proxy.conf.json`.

---

## ❓ Troubleshooting

- **"Command not found: ng"** — Run `npm install` in the FRONTEND/frontend folder to install Angular CLI locally.
- **Port already in use** — Change port with environment variables (e.g., `PORT=3001 npm run dev` for backend).
- **Node version too old** — Update Node.js to v18+ using [nvm](https://github.com/coreybutler/nvm-windows) (Windows) or [nvm-sh](https://github.com/nvm-sh/nvm) (macOS/Linux).

---

## 📝 License & Notes

This is an educational multimedia platform. For questions or contributions, please reach out to the development team.
