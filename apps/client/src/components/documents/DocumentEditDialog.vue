<template>
  <Dialog 
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
    header="Edytuj dokument"
    :modal="true" 
    class="edit-dialog-menu"
    style="width: 1500px"
  >
    <div v-if="document">
      <div class="p-fluid formgrid grid container">
        <div class="field col-6">
          <label for="type">Typ:</label>
          <InputText 
            id="type" 
            :modelValue="document.type"
            @update:modelValue="updateField('type', $event)"
          />
        </div>
        <div class="field col-6">
          <label for="date">Data</label>
          <InputText 
            id="date" 
            :modelValue="document.date"
            @update:modelValue="updateField('date', $event)"
          />
        </div>
        <div class="field col-6">
          <label for="firstName">Imię</label>
          <InputText 
            id="firstName" 
            :modelValue="document.firstName"
            @update:modelValue="updateField('firstName', $event)"
          />
        </div>
        <div class="field col-6">
          <label for="lastName">Nazwisko</label>
          <InputText 
            id="lastName" 
            :modelValue="document.lastName"
            @update:modelValue="updateField('lastName', $event)"
          />
        </div>
        <div class="field col-6">
          <label for="city">Miasto</label>
          <InputText 
            id="city" 
            :modelValue="document.city"
            @update:modelValue="updateField('city', $event)"
          />
        </div>
      </div>
    </div>

    <template #footer>
      <Button 
        label="Zapisz" 
        icon="pi pi-check" 
        class="p-button-success" 
        @click="$emit('save')" 
      />
    </template>
  </Dialog>
</template>

<script setup>
const props = defineProps({
  visible: Boolean,
  document: Object
})

const emit = defineEmits(['update:visible', 'update:document', 'save'])

const updateField = (field, value) => {
  emit('update:document', { ...props.document, [field]: value })
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