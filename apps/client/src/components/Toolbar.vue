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
          @click="store.loadDocsToDatabase"
        />
        
        <Button 
          label="Wyczyść dokumenty" 
          icon="pi pi-trash" 
          class="btn-clear"
          severity="danger"
          outlined
          @click="store.deleteAllDocuments"
        />
      </template>
    </Toolbar>
  </div>
</template>

<script setup>
import { useDocumentsStore } from '../stores/documents'
const store = useDocumentsStore()
</script>

<style scoped>

.custom-toolbar {
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

</style>