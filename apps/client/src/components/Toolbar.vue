<template>
  <div class="card">
    <Toolbar :pt="{ root: { class: 'custom-toolbar' } }">
      <template #start>
        <div class="logo">
          <span>ProfiSys</span>
        </div>
        
        <Button 
          label="Wczytaj dokumenty" 
          icon="pi pi-download" 
          class="btn-load"
          @click="loadAllDocuments"
        />
        
        <Button 
          label="Wyczyść dokumenty" 
          icon="pi pi-trash" 
          class="btn-clear"
          severity="danger"
          outlined
          @click="deleteAllDocuments"
        />
      </template>
    </Toolbar>
  </div>
</template>

<script>
import { ref } from 'vue'
import { clearAllDocuments, fetchAllDocuments } from '../api/documentsService'

export default {
  setup() {

    const documents = ref([])

    const loadAllDocuments = async() => {
      await fetchAllDocuments()
      console.log('Dokumenty zostały wczytane.')
    }

    const deleteAllDocuments = async () => {
      try {
        await clearAllDocuments() 
        documents.value = []       
        console.log('Dokumenty zostały usunięte.')
      } catch (error) {
        console.error('Błąd podczas usuwania dokumentów:', error)
      }
    }

    return { loadAllDocuments, deleteAllDocuments }
  }
}
</script>

<style scoped>

:deep(.custom-toolbar) {
  background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%);
  border: 1px solid #334155;
  border-radius: 12px;
  padding: 0.75rem 1.5rem;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.3);
  display: flex;
  height: 100px;
  align-items: center;
  gap: 1.5rem;
}

.logo {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 1.5rem;
  font-weight: 800;
  background: linear-gradient(135deg, #22d3ee 0%, #06b6d4 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  margin-right: 2rem;
  padding-right: 2rem;
  border-right: 2px solid #334155;
}

.logo i {
  font-size: 1.8rem;
  background: linear-gradient(135deg, #22d3ee 0%, #06b6d4 100%);
  -webkit-text-fill-color: transparent;
}

:deep(.btn-load) {
  background: linear-gradient(135deg, #06b6d4 0%, #0891b2 100%);
  border: none;
  color: white;
  padding: 0.75rem 1.5rem;
  font-weight: 600;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(6, 182, 212, 0.3);
  box-sizing: border-box;
}

:deep(.btn-clear) {
  border: 2px solid #ef4444;
  color: #ef4444;
  background: transparent;
  padding: 0.75rem 1.5rem;
  font-weight: 600;
  border-radius: 8px;
  box-sizing: border-box;
  margin-left: 30px;
}

:deep(.p-button-icon) {
  transition: transform 0.3s ease;
}

:deep(.btn-load:hover .p-button-icon) {
  transform: rotate(180deg);
}

:deep(.btn-clear:hover .p-button-icon) {
  transform: scale(1.2);
}
</style>