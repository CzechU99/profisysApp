<template>
  <Toolbar :pt="{ root: { class: 'custom-toolbar' } }">
    <template #start>
      <div class="logo"><span>ProfiSys</span></div>
      <DocumentActions />
      <LogoutButton />
    </template>
  </Toolbar>
</template>

<script setup>
import DocumentActions from './DocumentsActions.vue'
import LogoutButton from './LogoutButton.vue'
</script>

<style scoped>

.custom-toolbar {
  background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%);
  border: 1px solid #334155;
  border-radius: 12px;
  padding: 0.75rem 1.5rem;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.3);
  display: flex;
  height: 100px;
  align-items: center;
  gap: 1.5rem;
}

:deep(.btn-logout) {
  border: 1px solid #ef4444;
  color: #ef4444;
  background: transparent;
  padding: 0.75rem 1.5rem;
  border-radius: 5px;
  box-sizing: border-box;
  margin-left: 30px;
  transition: all 0.3s ease;
  height: 42px;
  position: absolute;
  right: 40px;
}

:deep(.btn-logout:hover) {
  background: #ef4444;
  color: white;
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(239, 68, 68, 0.4);
}

:deep(.btn-logout:active) {
  transform: translateY(0);
}

:deep(.btn-logout .p-button-icon) {
  transition: transform 0.3s ease;
}

:deep(.btn-logout:hover .p-button-icon) {
  transform: translateX(4px);
}

</style>
