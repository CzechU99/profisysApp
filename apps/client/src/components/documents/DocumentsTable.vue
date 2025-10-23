<template>
  <div>
    <DocumentContextMenu 
      ref="contextMenu"
      @edit="openEditDialog"
      @delete="handleDelete"
    />

    <DataTable 
      v-model:filters="filters"
      v-model:expandedRows="expandedRows"
      v-model:contextMenuSelection="selectedRow"
      :value="store.documents" 
      dataKey="id" 
      :paginator="true" 
      :rows="10" 
      removableSort 
      :rowsPerPageOptions="[5, 10, 25, 100]"
      :globalFilterFields="['id', 'type', 'date', 'firstName', 'lastName', 'city']"
      filterDisplay="row"
      class="tableDocuments"
      contextMenu
      @row-contextmenu="onRowContextMenu"
    >
      <template #header>
        <DocumentsTableHeader v-model="filters.global.value" />
      </template>

      <Column expander />
      <Column field="id" sortable header="ID" />
      <Column field="type" sortable header="TYP" />
      <Column field="date" sortable header="DATA" />
      <Column field="firstName" sortable header="IMIĘ" />
      <Column field="lastName" sortable header="NAZWISKO" />
      <Column field="city" sortable header="MIASTO" />

      <template #expansion="slotProps">
        <DocumentItemsTable :items="slotProps.data.documentItem" />
      </template>
    </DataTable>

    <DocumentEditDialog 
      v-model:visible="editDialogVisible"
      v-model:document="editedDocument"
      @save="handleSave"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { FilterMatchMode } from '@primevue/core/api'
import { useDocumentsStore } from '../../stores/documentsStore'
import DocumentContextMenu from './DocumentContextMenu.vue'
import DocumentsTableHeader from './DocumentsTableHeader.vue'
import DocumentItemsTable from './DocumentItemsTable.vue'
import DocumentEditDialog from './DocumentEditDialog.vue'

const store = useDocumentsStore()
const expandedRows = ref([])
const selectedRow = ref(null)
const contextMenu = ref()
const editDialogVisible = ref(false)
const editedDocument = ref(null)

const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS }
})

const onRowContextMenu = (event) => {
  contextMenu.value.show(event.originalEvent, selectedRow.value)
}

const openEditDialog = (document) => {
  editedDocument.value = JSON.parse(JSON.stringify(document))
  editDialogVisible.value = true
}

const handleDelete = (id) => {
  store.deleteDocumentById(id)
}

const handleSave = () => {
  store.updateDocuments(editedDocument.value)
  editDialogVisible.value = false
}
</script>

<style scoped>
:deep(.p-datatable-header) {
  padding: 0px !important;
}

:deep(.p-paginator-page-selected) {
  background: #1e293b !important;
  color: #22d3ee !important;
  font-weight: bold;
  font-size: 20px;
}

:deep(.p-paginator-rpp-dropdown):focus {
  border-color: #22d3ee !important;
}
</style>