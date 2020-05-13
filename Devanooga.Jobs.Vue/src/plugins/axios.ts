import { VueConstructor } from 'vue';
import { AxiosInstance } from 'axios';

export default function install(Vue: VueConstructor, axios?: AxiosInstance) {
    if (!axios) {
        return;
    }

    Vue.axios = axios;
    Vue.prototype.axios = axios;
    Vue.prototype.$http = axios;
}