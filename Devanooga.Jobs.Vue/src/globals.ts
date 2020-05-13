import { AxiosInstance } from 'axios';

declare module 'vue/types/vue' {
    interface Vue {
        $http: AxiosInstance
    }

    interface VueConstructor {
        axios: AxiosInstance
    }
}
