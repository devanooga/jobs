import axios, { AxiosError } from 'axios';

const axiosInstance = axios.create({
    baseURL: '/',
    withCredentials: true,
    headers: {
        common: {
            Accept: 'application/json'
        }
    }
});
export default axiosInstance;
