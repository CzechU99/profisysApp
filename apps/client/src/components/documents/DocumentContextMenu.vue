<template>
  <ContextMenu ref="cm" :model="menuModel" />
</template>

<script setup>
import { ref, computed } from 'vue'

const emit = defineEmits(['edit', 'delete'])
const cm = ref()
const selectedDocument = ref(null)

const menuModel = computed(() => [
  {
    label: 'Usuń',
    icon: 'pi pi-trash',
    command: () => emit('delete', selectedDocument.value.id)
  },
  {
    label: 'Edytuj',
    icon: 'pi pi-pencil',
    command: () => emit('edit', selectedDocument.value)
  }
])

const show = (event, document) => {
  selectedDocument.value = document
  cm.value.show(event)
}

defineExpose({ show })
</script>