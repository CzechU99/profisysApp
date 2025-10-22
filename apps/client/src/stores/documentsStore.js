import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { getAllDocuments, clearAllDocuments, fetchAllDocuments, deleteDocument } from '../api/documentsService'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const loading = ref(false)
  const error = ref(null)
  const toast = useToast()

  function clearDocuments() {
    documents.value = []
    loading.value = false
    error.value = null
  }

  function handleApiError(error) {
    if (error.response?.data?.message) {
      toast.error(error.response.data.message);
    } else if (error.response?.status === 403) {
      toast.error("Nie masz uprawnień do tej operacji!"); 
    } else if (error.response?.status === 401) {
      toast.error("Musisz się zalogować!"); 
    } else if (error.message) {
      toast.error("Błąd połączenia z serwerem!"); 
    } else {
      toast.error("Nieznany błąd");
    }
  }

  async function loadAllDocuments() {
    try {
      loading.value = true
      const response = await getAllDocuments()
      documents.value = response.data.documents
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      loading.value = false
    }
  }

  async function deleteAllDocuments() {
    try {
      const response = await clearAllDocuments()
      this.clearDocuments()
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function loadDocsToDatabase() {
    loading.value = true

    try {
      const responseCsvFiles = await fetchAllDocuments()
      toast.info(responseCsvFiles.data.message)
    } catch (error) {
      handleApiError(error)
    }

    try {
      const responseServer = await getAllDocuments()
      documents.value = responseServer.data.documents
      toast.info(responseServer.data.message)
    } catch (error) {
      handleApiError(error)
    }

    loading.value = false
  }

  async function deleteDocumentById(id) {
    try {
      const response = await deleteDocument(id)
      documents.value = documents.value.filter(doc => doc.id !== id)
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    documents,
    loading,
    error,
    loadAllDocuments,
    deleteAllDocuments,
    loadDocsToDatabase,
    deleteDocumentById,
    clearDocuments
  }
})
