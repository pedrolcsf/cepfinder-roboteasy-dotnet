<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated style="background: #004f71;">
      <q-toolbar>
        <q-toolbar-title>
          CEP Finder
        </q-toolbar-title>

        <div>Teste técnico - Roboteasy</div>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup>

defineOptions({
  name: 'MainLayout'
})

</script>
