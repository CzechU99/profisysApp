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

  <Dialog 
    v-model:visible="editDialogVisible" 
    header="Edytuj dokument"
    :modal="true" 
    class="edit-dialog-menu"
    style="width: 1500px"
  >
    <div v-if="editedDocument">
      <div class="p-fluid formgrid grid container">
        <div class="field col-6">
          <label for="type">Typ:</label><br>
          <InputText id="type" v-model="editedDocument.type" />
        </div>
        <div class="field col-6">
          <label for="date">Data</label><br>
          <InputText id="date" v-model="editedDocument.date" />
        </div>
        <div class="field col-6">
          <label for="firstName">Imię</label><br>
          <InputText id="firstName" v-model="editedDocument.firstName" />
        </div>
        <div class="field col-6">
          <label for="lastName">Nazwisko</label><br>
          <InputText id="lastName" v-model="editedDocument.lastName" />
        </div>
        <div class="field col-6">
          <label for="city">Miasto</label><br>
          <InputText id="city" v-model="editedDocument.city" />
        </div>
      </div>

    </div>

    <template #footer>
      <Button label="Zapisz" icon="pi pi-check" class="p-button-success" @click="saveEdit" />
    </template>
  </Dialog>
</template>


<script>
import { ref, computed } from 'vue'
import { FilterMatchMode } from '@primevue/core/api'
import { useDocumentsStore } from '../stores/documentsStore'

export default {
  setup() {
    const store = useDocumentsStore()
    const expandedRows = ref([])
    const selectedRow = ref(null)
    const cm = ref()
    const editDialogVisible = ref(false)
    const editedDocument = ref(null)
    
    const filters = ref({
      global: { value: null, matchMode: FilterMatchMode.CONTAINS }
    })

    const menuModel = computed(() => [
      {
        label: 'Usuń',
        icon: 'pi pi-trash',
        command: () => store.deleteDocumentById(selectedRow.value.id)
      },
      {
        label: 'Edytuj',
        icon: 'pi pi-pencil',
        command: () => {
          editedDocument.value = JSON.parse(JSON.stringify(selectedRow.value)) 
          editDialogVisible.value = true
        }
      }
    ])

    const onRowContextMenu = (event) => {
      cm.value.show(event.originalEvent)
    }

    const saveEdit = () => {
      store.updateDocuments(editedDocument.value)
      editDialogVisible.value = false
    }

    return { 
      store,
      filters,
      expandedRows,
      selectedRow,
      cm,
      menuModel,
      onRowContextMenu,
      saveEdit,
      editDialogVisible,
      editedDocument
    }
  }
}
</script>

<style scoped>

.field {
  float: left;
  margin-left: 40px;  
}

:deep(.p-datatable-header){
  padding: 0px !important;
}

.search-field :deep(input) {
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid #334155;
  color: white;
  padding: 0.75rem 1rem 0.75rem 2.5rem;
  border-radius: 8px;
  transition: all 0.3s ease;
}

.search-field :deep(input::placeholder) {
  color: #94a3b8;
}

.search-field :deep(input:focus) {
  background: rgba(255, 255, 255, 0.15);
  border-color: #22d3ee;
  box-shadow: 0 0 0 3px rgba(34, 211, 238, 0.2);
}

:deep(.p-paginator-page-selected){
  background: #1e293b !important;
  color: #22d3ee !important;
  font-weight: bold;
  font-size: 20px;
}

:deep(.p-paginator-rpp-dropdown):focus {
  border-color: #22d3ee !important;
}

</style>