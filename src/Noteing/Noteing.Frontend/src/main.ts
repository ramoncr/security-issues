import './assets/main.css'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import '@vueup/vue-quill/dist/vue-quill.snow.css';

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { QuillEditor } from '@vueup/vue-quill'

const app = createApp(App)

app.component('QuillEditor', QuillEditor);

app.use(router)

app.mount('#app')
