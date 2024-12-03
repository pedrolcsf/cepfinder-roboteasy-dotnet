# **CEP Finder**

Este projeto consiste em uma aplicação web para consulta de CEP, utilizando um backend em **Spring Boot** e um frontend em **VueJS com Quasar**. A aplicação consome a API pública do [ViaCEP](https://viacep.com.br/) e implementa caching e conteinerização com **Docker**.

## **Descrição**

O **CEP Finder** foi desenvolvido para o teste técnico roboteasy

---

## **Tecnologias Utilizadas**

### Frontend
- VueJS
- Quasar
- Axios

### Outros
- Docker & Docker Compose
- NGINX
- ViaCEP API

---

## **Funcionalidades**
- Consulta de CEPs.
- Exibição de informações detalhadas do endereço:
  - Rua
  - Bairro
  - Cidade
  - Estado
- Caching para evitar múltiplas consultas à mesma informação.
- Tratamento de erros para CEPs inválidos ou não encontrados.

---
#### **Frontend**
1. Certifique-se de ter o **Node.js** e o **npm** instalados.
2. Navegue para a pasta do frontend:
   ```bash
   cd frontend
   ```
3. Instale as dependências:
   ```bash
   npm install
   ```
4. Inicie o servidor de desenvolvimento:
   ```bash
   npm run dev
   ```
5. O frontend estará disponível em: [http://localhost:9000](http://localhost:9000)

---