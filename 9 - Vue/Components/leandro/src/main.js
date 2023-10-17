import { createApp } from 'vue';
import App from './App.vue';

const app = createApp(App);
app.mount('#app');

// Third party package
app.use(require('vue-moment'));
