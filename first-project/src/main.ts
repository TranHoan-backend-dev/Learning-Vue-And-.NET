import {createApp} from 'vue'
import {registerPlugins} from '@/plugins'
import App from './App.vue'
import 'unfonts.css'
import './assets/css/style.css'
import {router} from "@/router/route.ts";

const app = createApp(App)

registerPlugins(app)

app.mount('#app')
app.use(router)
