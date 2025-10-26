<template>
  <Dialog 
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
    header="Dodaj dokument"
    :modal="true" 
    class="edit-dialog-menu"
    style="width: 1500px"
  >
    <div v-if="localDocument">
      <div class="p-fluid formgrid grid container">
        <div class="field col-6">
          <label for="type">Typ:</label>
          <InputText 
            inputId="type"
            name="type"
            v-model="localDocument.type"
          />
        </div>
        <div class="field col-6">
          <label for="date">Data</label>
          <InputText 
            inputId="date"
            type="date"
            name="date"
            v-model="localDocument.date"
          />
        </div>
        <div class="field col-6">
          <label for="firstName">Imię</label>
          <InputText 
            inputId="firstName"
            name="firstName"
            v-model="localDocument.firstName"
          />
        </div>
        <div class="field col-6">
          <label for="lastName">Nazwisko</label>
          <InputText 
            inputId="lastName"
            name="lastName"
            v-model="localDocument.lastName"
          />
        </div>
        <div class="field col-6">
          <label for="city">Miasto</label>
          <InputText 
            inputId="city"
            name="city"
            v-model="localDocument.city"
          />
        </div>
      </div>
    </div>
    
    <template #footer>
      <Button 
        label="Zapisz"
        icon="pi pi-check"
        class="p-button-success"
        @click="handleSave"
      />
    </template>
  </Dialog>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
  visible: Boolean,
  document: Object
})

const emit = defineEmits(['update:visible', 'update:document', 'save'])

const localDocument = ref({
  type: '',
  date: new Date().toISOString().split('T')[0],
  firstName: '',
  lastName: '',
  city: ''
})

watch(() => props.document, (newVal) => {
  if (newVal) {
    localDocument.value = {
      type: newVal.type || '',
      date: newVal.date || new Date().toISOString().split('T')[0],
      firstName: newVal.firstName || '',
      lastName: newVal.lastName || '',
      city: newVal.city || '',
    }
  }
}, { immediate: true, deep: true })

const handleSave = () => {
  emit('update:document', localDocument.value)
  emit('save')
  emit('update:visible', false)
}
</script>

<style scoped>
.field {
  float: left;
  margin-left: 40px;  
}

.field label {
  display: block;
  margin-bottom: 0.5rem;
}
</style>