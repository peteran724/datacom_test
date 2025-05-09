import axios from 'axios';

const api = axios.create({ baseURL: 'https://localhost:7092/api' });

export const getApplications = (pageNumber, pageSize) => 
  api.get(`/applications?pageNumber=${pageNumber}&pageSize=${pageSize}`);

export const addApplication = (app) => api.post('/applications', app);

export const updateApplication = (id, status) => api.patch(`/applications/${id}/status/${status}`);
