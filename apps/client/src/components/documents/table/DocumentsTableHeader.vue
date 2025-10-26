<template>
  <div class="table-header">
    <div class="header-title">
      <i class="pi pi-file-edit"></i>
      <span>Dokumenty</span>
    </div>
    <IconField class="search-field">
      <InputIcon class="pi pi-search" />
      <InputText 
        :modelValue="modelValue"
        @update:modelValue="$emit('update:modelValue', $event)"
        placeholder="Szukaj dokumentów..." 
      />
    </IconField>
  </div>
</template>

<script setup>
defineProps({
  modelValue: {
    type: String,
    default: null
  }
})

defineEmits(['update:modelValue'])
</script>

<style scoped>
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
</style>