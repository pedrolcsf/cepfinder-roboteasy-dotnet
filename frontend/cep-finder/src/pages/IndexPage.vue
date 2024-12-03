<template>
  <q-page class="flex flex-center">
    <div class="q-pa-md" style="max-width: 500px; width: 100%;">
      <q-input
        v-model="cep"
        placeholder="Digite o CEP"
        class="q-mb-md"
        :loading="loading"
        autofocus
        dense
        clearable
      />
      <q-btn 
        :disable="loading" 
        label="Buscar" 
        @click="fetchCep" 
        style="background-color: #40b494; color: #fff" 
        class="full-width" 
      />

      <div class="q-mt-md">
        <q-card v-if="address">
          <q-card-section>
            <div class="text-h6 text-weight-bold">{{ address.logradouro }}</div>
            <div class="text-subtitle2">{{ address.bairro }}, {{ address.localidade }} - {{ address.uf }}</div>
            <div class="q-mt-xs"><strong>CEP:</strong> {{ address.cep }}</div>
            <q-separator />
            <div><strong>Complemento:</strong> {{ address.complemento || 'Não disponível' }}</div>
            <div><strong>Tipo de Logradouro:</strong> {{ address.tipo_logradouro || 'Não informado' }}</div>
            <div><strong>IBGE:</strong> {{ address.ibge || 'Não disponível' }}</div>
            <div><strong>DDD:</strong> {{ address.ddd || 'Não disponível' }}</div>
          </q-card-section>
        </q-card>

        <q-alert v-if="error == null" color="negative" class="q-mt-md" dense>{{ error }}</q-alert>

        <q-spinner v-if="loading && !error" />
      </div>

      <div class="q-mt-md">
        <q-card v-if="recentCeps.length > 0">
          <q-card-section>
            <div class="text-h6">Últimos CEPs Consultados</div>
            <q-list>
              <q-item v-for="(cep, index) in recentCeps" :key="index" clickable @click="fetchCepFromList(cep)">
                <q-item-section>{{ cep }}</q-item-section>
              </q-item>
            </q-list>
          </q-card-section>
        </q-card>
      </div>

      <q-btn 
        label="Limpar Histórico" 
        @click="clearRecentCeps" 
        style="background-color: #e74c3c; color: #fff" 
        class="full-width q-mt-md" 
      />
    </div>
  </q-page>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const cep = ref('');
const address = ref(null);
const loading = ref(false);
const error = ref(null);
const recentCeps = ref([""]);

const fetchCep = async () => {
  cep.value = cep.value.replace(/\D/g, '');
  loading.value = true;
  try {
    const { data } = await axios.get(`http://localhost:8080/api/cep/${cep.value}`);
    address.value = data;
    addCepToLocalStorage(cep.value);
  } catch (err) {
    error.value = 'CEP não encontrado ou inválido!';
  } finally {
    loading.value = false;
  }
};

const loadRecentCeps = () => {
  const storedCeps = localStorage.getItem('recentCeps');
  recentCeps.value = storedCeps ? JSON.parse(storedCeps) : [];
};

const addCepToLocalStorage = (newCep) => {
  if (!recentCeps.value.includes(newCep)) {
    recentCeps.value = [newCep, ...recentCeps.value].slice(0, 5);
    localStorage.setItem('recentCeps', JSON.stringify(recentCeps.value));
  }
};

const fetchCepFromList = (cepFromList) => {
  cep.value = cepFromList;
  setTimeout(() => fetchCep(), 0);
};

const clearRecentCeps = () => {
  localStorage.removeItem('recentCeps');
  recentCeps.value = [];
};

onMounted(loadRecentCeps);
</script>

<style scoped>
.q-page {
  background-color: #f5f5f5;
}

.q-card {
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.q-card-section {
  padding: 16px;
}

.text-h6 {
  color: #004f71;
}

.text-subtitle2 {
  color: #555;
}

.q-input {
  font-size: 1rem;
}

.q-btn {
  font-size: 1rem;
}

.q-alert {
  margin-top: 16px;
}

.q-separator {
  margin: 8px 0;
}

.q-card-section > div {
  margin-bottom: 8px;
}

.q-list {
  max-height: 200px;
  overflow-y: auto;
}

.q-item {
  cursor: pointer;
}

.q-item:hover {
  background-color: #f5f5f5;
}
</style>
