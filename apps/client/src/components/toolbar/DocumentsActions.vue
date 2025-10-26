<template>
  <div class="document-actions">
    <Button 
      label="Wczytaj dokumenty" 
      icon="pi pi-download" 
      class="btn-load"
      @click="documentsStore.importFromCsv"
      v-if="authStore.isAdmin"
    />
    <Button 
      label="Wyczyść dokumenty" 
      icon="pi pi-trash" 
      class="btn-clear"
      severity="danger"
      outlined
      @click="documentsStore.deleteAll"
      v-if="authStore.isAdmin"
    />
    <Button
      label="Dodaj dokument"
      icon="pi pi-plus"
      class="btn-add"
      severity="success"
      @click="$emit('open-add-document')"
      v-if="authStore.isAdmin"
    />
  </div>
</template>

<script setup>
import { useDocumentsStore } from '../../stores/documentsStore'
import { useAuthStore } from '../../stores/authStore'

const documentsStore = useDocumentsStore()
const authStore = useAuthStore()

defineEmits(['open-add-document'])
</script>

<style scoped>
.btn-add {
  background: linear-gradient(135deg, #22d3ee 0%, #3b82f6 100%);
  color: white;
  border: 0px;
  border-radius: 5px;
  padding: 0.75rem 1.5rem;
  font-weight: bold;
  transition: all 0.3s ease;
  align-items: center;
  gap: 0.5rem;
  margin-left: 30px;
}
</style>
