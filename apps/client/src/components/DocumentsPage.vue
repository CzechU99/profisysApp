<template>

  <Toolbar />

  <div class="tableContainer">
    <DocumentsTable />
  </div>
  
</template>

<script>
import DocumentsTable from '../components/DocumentsTable.vue';
import Toolbar from '../components/Toolbar.vue';
import { useDocumentsStore } from '../stores/documents'

export default {
  components: { DocumentsTable, Toolbar },
  setup() {
    const store = useDocumentsStore()
    return { store }
  }
}
</script>