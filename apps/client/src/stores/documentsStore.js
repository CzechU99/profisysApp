import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { 
  fetchAll as apiFetchAll, 
  deleteAll as apiDeleteAll, 
  importFromCsv as apiImportFromCsv, 
  deleteById as apiDeleteById, 
  update as apiUpdate, 
  create as apiCreate 
} from '../api/documentsService'
import { handleApiError } from '../utils/errorHandler'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const isLoading = ref(false)
  const isDeleting = ref(false)
  const toast = useToast()

  function clear() {
    documents.value = []
    isLoading.value = false
  }

  async function fetchAll() {
    try {
      isLoading.value = true
      const response = await apiFetchAll()
      documents.value = response.data.documents.sort((a, b) => new Date(b.date) - new Date(a.date))
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      isLoading.value = false
    }
  }

  async function deleteAll() {
    isDeleting.value = true
    try {
      const response = await apiDeleteAll()
      clear()
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      isDeleting.value = false
    }
  }

  async function importFromCsv() {
    isLoading.value = true
    try {
      const importResponse = await apiImportFromCsv()
      toast.info(importResponse.data.message)
      
      const fetchResponse = await apiFetchAll()
      documents.value = fetchResponse.data.documents.sort((a, b) => new Date(b.date) - new Date(a.date))
      toast.info(fetchResponse.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      isLoading.value = false
    }
  }

  async function remove(id) {
    try {
      const response = await apiDeleteById(id)
      documents.value = documents.value.filter(doc => doc.id !== id)
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function update(document) {
    try {
      const response = await apiUpdate(document)
      const index = documents.value.findIndex(d => d.id === document.id)
      if (index !== -1) documents.value[index] = { ...document }
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function create(documentData) {
    try {
      const response = await apiCreate(documentData)
      documentData.id = response.data.documentId
      documents.value.unshift(documentData)
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    documents,
    isLoading,
    isDeleting,
    fetchAll,
    deleteAll,
    importFromCsv,
    remove,
    update,
    create,
    clear
  }
})