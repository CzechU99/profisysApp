<template>
  <div>
    <ContextMenu ref="cm" :model="menuModel" />
    
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
        <div class="table-header">
          <div class="header-title">
            <i class="pi pi-file-edit"></i>
            <span>Dokumenty</span>
          </div>
          <IconField class="search-field">
            <InputIcon class="pi pi-search" />
            <InputText v-model="filters.global.value" placeholder="Szukaj dokumentów..." />
          </IconField>
        </div>
      </template>

      <Column expander />
      <Column field="id" sortable header="ID" />
      <Column field="type" sortable header="TYP" />
      <Column field="date" sortable header="DATA" />
      <Column field="firstName" sortable header="IMIĘ" />
      <Column field="lastName" sortable header="NAZWISKO" />
      <Column field="city" sortable header="MIASTO" />

      <template #expansion="slotProps">
        <div class="expansion-wrapper">
          <DataTable 
            :value="slotProps.data.documentItem" 
            dataKey="id" 
            removableSort 
            class="subTable"
          >
            <Column field="product" sortable header="PRODUKT" />
            <Column field="quantity" sortable header="ILOŚĆ" />
            <Column field="taxRate" sortable header="PODATEK" />
            <Column field="price" sortable header="CENA" />
          </DataTable>
        </div>
      </template>

    </DataTable>
  </div>
</template>

<script>
import { ref, computed } from 'vue'
import { FilterMatchMode } from '@primevue/core/api'
import { useDocumentsStore } from '../stores/documents'

export default {
  setup() {
    const store = useDocumentsStore()
    const expandedRows = ref([])
    const selectedRow = ref(null)
    const cm = ref()
    
    const filters = ref({
      global: { value: null, matchMode: FilterMatchMode.CONTAINS }
    })

    const menuModel = computed(() => [
      {
        label: 'Usuń',
        icon: 'pi pi-trash',
        command: () => store.deleteDocumentById(selectedRow.value.id)
      }
    ])

    const onRowContextMenu = (event) => {
      cm.value.show(event.originalEvent)
    }

    return { 
      store,
      filters,
      expandedRows,
      selectedRow,
      cm,
      menuModel,
      onRowContextMenu
    }
  }
}
</script>

<style scoped>
.tableDocuments {
  width: 90%;
  margin: 2rem auto;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.4);
}

.table-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px !important;
  background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%);
  border-bottom: 2px solid #22d3ee;
}

:deep(.p-datatable-header){
  padding: 0px !important;
}

.header-title {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-size: 1.5rem;
  font-weight: 700;
  margin-left: 20px;
  color: #22d3ee;
}

.header-title i {
  font-size: 1.8rem;
  color: #22d3ee;
}

.search-field {
  min-width: 300px;
}

.search-field :deep(input) {
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid #334155;
  color: white;
  padding: 0.75rem 1rem 0.75rem 2.5rem;
  border-radius: 8px;
  transition: all 0.3s ease;
}

:deep(.p-paginator-page-selected){
  background: #1e293b !important;
  color: #22d3ee !important;
  font-weight: bold;
  font-size: 20px;
}

.search-field :deep(input::placeholder) {
  color: #94a3b8;
}

.search-field :deep(input:focus) {
  background: rgba(255, 255, 255, 0.15);
  border-color: #22d3ee;
  box-shadow: 0 0 0 3px rgba(34, 211, 238, 0.2);
}

:deep(.p-paginator-rpp-dropdown):focus {
  border-color: #22d3ee !important;
}

.search-field :deep(.p-icon) {
  color: #22d3ee;
}

/* Context menu highlight */
:deep(.p-datatable-tbody > tr.p-contextmenu-selected) {
  background: rgba(34, 211, 238, 0.2) !important;
}

/* Expansion wrapper */
.expansion-wrapper {
  background: linear-gradient(135deg, #0f172a 0%, #020617 100%);
  padding: 1.5rem;
  margin: 0.5rem 0;
  border-left: 4px solid #22d3ee;
  border-radius: 8px;
}

.subTable {
  margin-left: 2rem;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
}

.subTable :deep(thead) {
  background: linear-gradient(135deg, #334155 0%, #1e293b 100%);
}

.subTable :deep(th) {
  background: linear-gradient(135deg, #334155 0%, #1e293b 100%);
  color: #67e8f9;
  font-weight: 600;
  padding: 0.875rem;
  border-bottom: 2px solid #475569;
  font-size: 0.8125rem;
}

.subTable :deep(tbody tr) {
  background: #1e293b;
  border-bottom: 1px solid #334155;
}

.subTable :deep(tbody tr:hover) {
  background: #334155;
}

.subTable :deep(tbody td) {
  color: #cbd5e1;
  padding: 0.875rem;
}
</style>