// src/services/apiService.ts
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';

export interface ApiRequestParams {
  api: AxiosRequestConfig;
  onSuccess?: (data: any, response: AxiosResponse) => void;
  onError?: (data: any, response: AxiosResponse) => void;
  onRequestError?: (error: any, response?: AxiosResponse) => void;
}

export async function createRequest({
  api,
  onSuccess,
  onError,
  onRequestError,
}: ApiRequestParams): Promise<void> {
  try {
    const token = localStorage.getItem('token');

    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      delete axios.defaults.headers.common['Authorization'];
    }

    const response = await axios.request(api);

    if (response.status === 200) {
      onSuccess && onSuccess(response.data, response);
    } else {
      onError && onError(response.data, response);
    }
  } catch (error: any) {
    const response = error.response;

    if (onRequestError) {
      onRequestError(error, response);
    } else {
      console.error('Request failed:', error);
    }
  }
}
