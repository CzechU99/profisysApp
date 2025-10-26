import api from './axios'

const API_URL = import.meta.env.VITE_API_BASE_URL

export const fetchAll = async () => {
  return await api.get(`${API_URL}/documents`)
}

export const importFromCsv = async () => {
  return await api.post(`${API_URL}/dataImport/csvFiles`)
}

export const deleteAll = async () => {
  return await api.delete(`${API_URL}/documents`)
}

export const deleteById = async (documentId) => {
  return await api.delete(`${API_URL}/documents/${documentId}`)
}

export const update = async (document) => {
  return await api.put(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  })
}

export const create = async (document) => {
  return await api.post(`${API_URL}/documents`, document, {
    headers: { 'Content-Type': 'application/json' }
  })
}