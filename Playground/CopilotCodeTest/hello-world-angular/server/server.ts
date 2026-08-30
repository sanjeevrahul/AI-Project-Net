import express from 'express';
import cors from 'cors';
import { genkit } from 'genkit';
import { ollama } from 'genkitx-ollama';

const ai = genkit({
  plugins: [
    ollama(),
  ],
});

const app = express();

app.use(cors());
app.use(express.json());

app.post('/api/chat', async (req, res) => {
  try {
    const prompt = req.body.prompt ?? 'Why is the sky blue?';

    console.log('Prompt:', prompt);

    const result = await ai.generate({
      model: ollama.model('llama3.2:latest'),
      prompt,
    });

    console.log('Response:', result.text);

    res.json({
      text: result.text,
    });
  } catch (error) {
    console.error('Genkit/Ollama error:', error);

    res.status(500).json({
      error: String(error),
    });
  }
});

app.listen(3000, () => {
  console.log('Genkit server running at http://localhost:3000');
});