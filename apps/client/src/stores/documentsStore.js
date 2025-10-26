import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { getAllDocuments, clearAllDocuments, fetchAllDocuments, 
  deleteDocument, updateDocument, addDocument } from '../api/documentsService'
import { handleApiError } from '../utils/errorHandler'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const loading = ref(false)
  const deleting = ref(false)
  const toast = useToast()

  function clearDocuments() {
    documents.value = []
    loading.value = false
  }

  async function loadAllDocuments() {
    try {
      loading.value = true
      const response = await getAllDocuments()
      documents.value = response.data.documents.sort((a, b) => new Date(b.date) - new Date(a.date))
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      loading.value = false
    }
  }

  async function deleteAllDocuments() {
    deleting.value = true
    try {
      const response = await clearAllDocuments()
      clearDocuments()
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      deleting.value = false
    }
  }

  async function loadDocsToDatabase() {
    loading.value = true
    try {
      const responseCsvFiles = await fetchAllDocuments()
      toast.info(responseCsvFiles.data.message)
      
      const responseServer = await getAllDocuments()
      documents.value = responseServer.data.documents.sort((a, b) => new Date(b.date) - new Date(a.date))
      toast.info(responseServer.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      loading.value = false
    }
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

  async function updateDocuments(document) {
    try {
      const response = await updateDocument(document)
      const index = documents.value.findIndex(d => d.id === document.id)
      if (index !== -1) documents.value[index] = { ...document }
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function addDocuments(documentData) {
    try {
      const response = await addDocument(documentData)
      documentData.id = response.data.documentId
      documents.value.unshift(documentData)
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    documents,
    loading,
    deleting,
    loadAllDocuments,
    deleteAllDocuments,
    loadDocsToDatabase,
    deleteDocumentById,
    clearDocuments,
    updateDocuments,
    addDocuments
  }
})