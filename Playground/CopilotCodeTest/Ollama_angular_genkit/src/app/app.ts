import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  message = signal('Click the button');

  async onButtonClick(): Promise<void> {
    this.message.set('Calling Ollama...');

    try {
      const response = await fetch('http://localhost:3000/api/chat', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          prompt: 'Why is the sky blue?',
        }),
      });

      const data = await response.json();

      console.log('Ollama response:', data);

      this.message.set(data.text);
    } catch (error) {
      console.error(error);
      this.message.set('Error: ' + error);
    }
  }
}