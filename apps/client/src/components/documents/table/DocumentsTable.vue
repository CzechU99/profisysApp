<template>
  <div>
    <DocumentContextMenu
      ref="contextMenu"
      @delete="handleDelete"
      @edit="openEditDialog"
      @add="openAddDialogItem"
      @delete-item="deleteDocumentItem"
      @edit-item="openEditDialogItem"
    />

    <DataTable 
      v-model:filters="filters"
      v-model:expandedRows="expandedRows"
      v-model:contextMenuSelection="selectedDocument"
      :value="documentsStore.documents" 
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
        <DocumentItemsTable 
          :items="slotProps.data.documentItem"
          @item-context-menu="onItemContextMenu"
        />
      </template>

    </DataTable>

    <DocumentEditDialog 
      v-model:visible="editDialogVisible"
      v-model:document="editedDocument"
      @save="handleSave"
    />

    <ItemEditDialog 
      v-model:visible="editItemDialogVisible"
      v-model:item="editedItem"
      @save="handleItemSave"
    />

    <ItemAddDialog 
      v-model:visible="addItemDialogVisible"
      v-model:item="addItem"
      @save="handleAddItemSave"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { FilterMatchMode } from '@primevue/core/api'
import { useDocumentsStore } from '../../../stores/documentsStore'
import { useDocumentItemsStore } from '../../../stores/documentItemsStore'

import DocumentContextMenu from '../context-menu/ContextMenu.vue'
import DocumentsTableHeader from './DocumentsTableHeader.vue'
import DocumentItemsTable from './DocumentItemsTable.vue'
import DocumentEditDialog from '../dialogs/DocumentEditDialog.vue'
import ItemEditDialog from '../dialogs/ItemEditDialog.vue'
import ItemAddDialog from '../dialogs/ItemAddDialog.vue'

const documentsStore = useDocumentsStore()
const itemsStore = useDocumentItemsStore();

const expandedRows = ref([])
const selectedDocument = ref(null)
const contextMenu = ref()
const editDialogVisible = ref(false)
const editItemDialogVisible = ref(false)
const addItemDialogVisible = ref(false)
const editedDocument = ref(null)
const editedItem = ref(null)
const addItem = ref(null)

const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS }
})

const onRowContextMenu = (event) => {
  contextMenu.value.show(event.originalEvent, event.data, 'document')
}

const onItemContextMenu = (event) => {
  contextMenu.value.show(event.originalEvent, event.data, 'item')
}

const openEditDialog = (document) => {
  editedDocument.value = JSON.parse(JSON.stringify(document))
  editDialogVisible.value = true
}

const openEditDialogItem = (item) => {
  editedItem.value = JSON.parse(JSON.stringify(item))
  editItemDialogVisible.value = true
}

const openAddDialogItem = (document) => {
  addItem.value = {
    documentId: document.id   
  };
  addItemDialogVisible.value = true;
}

const handleDelete = (id) => {
  documentsStore.deleteDocumentById(id)
}

const deleteDocumentItem = (id) => {
  itemsStore.deleteDocumentItemById(id)
}

const handleSave = () => {
  documentsStore.updateDocuments(editedDocument.value)
  editDialogVisible.value = false
}

const handleItemSave = () => {
  itemsStore.updateItems(editedItem.value)
  editItemDialogVisible.value = false
}

const handleAddItemSave = () => {
  itemsStore.addItems(addItem.value)
  addItemDialogVisible.value = false
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