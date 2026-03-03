import express from "express";

const app = express();
const PORT = process.env.PORT ? Number(process.env.PORT) : 3000;

app.get("/", (_req, res) => {
  res.send("Hello from the TypeScript server");
});

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});
