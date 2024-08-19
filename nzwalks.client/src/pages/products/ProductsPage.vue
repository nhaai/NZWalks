<script setup lang="ts">
import { ref } from 'vue'
import ProductsTable from './widgets/ProductsTable.vue'
import EditProductForm from './widgets/EditProductForm.vue'
import { Product } from './types'
import { useProducts } from './composables/useProducts'
import { useModal, useToast } from 'vuestic-ui'

const doShowEditProductModal = ref(false)

const { products, isLoading, filters, sorting, pagination, ...productsApi } = useProducts()

const productToEdit = ref<Product | null>(null)

const showEditProductModal = (product: Product) => {
  productToEdit.value = product
  doShowEditProductModal.value = true
}

const showAddProductModal = () => {
  productToEdit.value = null
  doShowEditProductModal.value = true
}

const { init: notify } = useToast()

const onProductSaved = async (product: Product) => {
  if (productToEdit.value) {
    await productsApi.update(product)
    notify({
      message: `${product.name} has been updated`,
      color: 'success',
    })
  } else {
    productsApi.add(product)
    notify({
      message: `${product.name} has been created`,
      color: 'success',
    })
  }
}

const onProductDelete = async (product: Product) => {
  await productsApi.remove(product)
  notify({
    message: `${product.name} has been deleted`,
    color: 'success',
  })
}

const editFormRef = ref()

const { confirm } = useModal()

const beforeEditFormModalClose = async (hide: () => unknown) => {
  if (editFormRef.value.isFormHasUnsavedChanges) {
    const agreed = await confirm({
      maxWidth: '380px',
      message: 'Form has unsaved changes. Are you sure you want to close it?',
      size: 'small',
    })
    if (agreed) {
      hide()
    }
  } else {
    hide()
  }
}
</script>

<template>
  <h1 class="page-title">Products</h1>

  <VaCard>
    <VaCardContent>
      <div class="flex flex-col md:flex-row gap-2 mb-2 justify-between">
        <div class="flex flex-col md:flex-row gap-2 justify-start">
          <VaButtonToggle
            v-show="false"
            v-model="filters.isActive"
            color="background-element"
            border-color="background-element"
            :options="[
              { label: 'Active', value: true },
              { label: 'Inactive', value: false },
            ]"
          />
          <VaInput v-model="filters.search" placeholder="Search">
            <template #prependInner>
              <VaIcon name="search" color="secondary" size="small" />
            </template>
          </VaInput>
        </div>
        <VaButton @click="showAddProductModal">Add Product</VaButton>
      </div>

      <ProductsTable
        v-model:sort-by="sorting.sortBy"
        v-model:sorting-order="sorting.sortingOrder"
        :products="products"
        :loading="isLoading"
        :pagination="pagination"
        @editProduct="showEditProductModal"
        @deleteProduct="onProductDelete"
      />
    </VaCardContent>
  </VaCard>

  <VaModal
    v-slot="{ cancel, ok }"
    v-model="doShowEditProductModal"
    size="small"
    mobile-fullscreen
    close-button
    hide-default-actions
    :before-cancel="beforeEditFormModalClose"
  >
    <h1 class="va-h5">{{ productToEdit ? 'Edit product' : 'Add product' }}</h1>
    <EditProductForm
      ref="editFormRef"
      :product="productToEdit"
      :save-button-label="productToEdit ? 'Save' : 'Add'"
      @close="cancel"
      @save="
        (product) => {
          onProductSaved(product)
          ok()
        }
      "
    />
  </VaModal>
</template>
