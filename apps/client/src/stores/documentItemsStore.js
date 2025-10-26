import { defineStore } from 'pinia'
import { useToast } from 'vue-toastification'
import { deleteDocumentItem, updateItem, addItem } from '../api/itemsService'
import { handleApiError } from '../utils/errorHandler'
import { useDocumentsStore } from './documentsStore'

export const useDocumentItemsStore = defineStore('documentItems', () => {
  const toast = useToast()

  async function updateItems(item) {
    try {
      const response = await updateItem(item)
      const documentsStore = useDocumentsStore()
      
      const document = documentsStore.documents.find(d => 
        d.documentItem?.some(i => i.id === item.id)
      )
      
      if (document) {
        const itemIndex = document.documentItem.findIndex(i => i.id === item.id)
        if (itemIndex !== -1) {
          document.documentItem[itemIndex] = { ...item }
        }
      }
      
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function deleteDocumentItemById(itemId) {
    try {
      const response = await deleteDocumentItem(itemId)
      const documentsStore = useDocumentsStore()

      const document = documentsStore.documents.find(d =>
        d.documentItem.some(i => i.id === itemId)
      )

      if (document) {
        document.documentItem = document.documentItem.filter(i => i.id !== itemId)
      }

      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function addItems(item) {
    try {
      const response = await addItem(item)
      const documentsStore = useDocumentsStore()
      
      item.id = response.data.itemId

      const document = documentsStore.documents.find(d => d.id === item.documentId)
      if (document) {
        if (!document.documentItem) {
          document.documentItem = []
        }
        document.documentItem.push(item)
      }
      
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    updateItems,
    deleteDocumentItemById,
    addItems
  }
})