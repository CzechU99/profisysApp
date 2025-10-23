<template>
  <div class="document-actions">
    <Button 
      label="Wczytaj dokumenty" 
      icon="pi pi-download" 
      class="btn-load"
      @click="store.loadDocsToDatabase"
      v-if="auth.isAdmin"
    />
    <Button 
      label="Wyczyść dokumenty" 
      icon="pi pi-trash" 
      class="btn-clear"
      severity="danger"
      outlined
      @click="store.deleteAllDocuments"
      v-if="auth.isAdmin"
    />
  </div>
</template>

<script setup>
import { useDocumentsStore } from '../../stores/documentsStore'
import { useAuthStore } from '../../stores/authStore'

const store = useDocumentsStore()
const auth = useAuthStore()
</script>
