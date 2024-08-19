<script setup lang="ts">
import { ref } from 'vue'
import CategoriesTable from './widgets/CategoriesTable.vue'
import EditCategoryForm from './widgets/EditCategoryForm.vue'
import { Category } from './types'
import { useCategories } from './composables/useCategories'
import { useModal, useToast } from 'vuestic-ui'

const doShowEditCategoryModal = ref(false)

const { categories, isLoading, filters, sorting, pagination, ...categoriesApi } = useCategories()

const categoryToEdit = ref<Category | null>(null)

const showEditCategoryModal = (category: Category) => {
  categoryToEdit.value = category
  doShowEditCategoryModal.value = true
}

const showAddCategoryModal = () => {
  categoryToEdit.value = null
  doShowEditCategoryModal.value = true
}

const { init: notify } = useToast()

const onCategorySaved = async (category: Category) => {
  if (categoryToEdit.value) {
    await categoriesApi.update(category)
    notify({
      message: `${category.name} has been updated`,
      color: 'success',
    })
  } else {
    categoriesApi.add(category)
    notify({
      message: `${category.name} has been created`,
      color: 'success',
    })
  }
}

const onCategoryDelete = async (category: Category) => {
  await categoriesApi.remove(category)
  notify({
    message: `${category.name} has been deleted`,
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
  <h1 class="page-title">Categories</h1>

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
        <VaButton @click="showAddCategoryModal">Add Category</VaButton>
      </div>

      <CategoriesTable
        v-model:sort-by="sorting.sortBy"
        v-model:sorting-order="sorting.sortingOrder"
        :categories="categories"
        :loading="isLoading"
        :pagination="pagination"
        @editCategory="showEditCategoryModal"
        @deleteCategory="onCategoryDelete"
      />
    </VaCardContent>
  </VaCard>

  <VaModal
    v-slot="{ cancel, ok }"
    v-model="doShowEditCategoryModal"
    size="small"
    mobile-fullscreen
    close-button
    hide-default-actions
    :before-cancel="beforeEditFormModalClose"
  >
    <h1 class="va-h5">{{ categoryToEdit ? 'Edit category' : 'Add category' }}</h1>
    <EditCategoryForm
      ref="editFormRef"
      :category="categoryToEdit"
      :save-button-label="categoryToEdit ? 'Save' : 'Add'"
      @close="cancel"
      @save="
        (category) => {
          onCategorySaved(category)
          ok()
        }
      "
    />
  </VaModal>
</template>
