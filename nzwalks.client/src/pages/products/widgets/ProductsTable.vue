<script setup lang="ts">
import { defineVaDataTableColumns, useModal } from 'vuestic-ui'
import { Product } from '../types'
import { PropType, computed, toRef } from 'vue'
import { Pagination, Sorting } from '../../../data/pages/products'
import { useVModel } from '@vueuse/core'

const columns = defineVaDataTableColumns([
  { label: 'Name', key: 'name', sortable: true },
  { label: 'Brand', key: 'brand' },
  { label: 'In Stock', key: 'quantity' },
  { label: 'Unit Price ($)', key: 'unitPrice' },
  { label: 'Purchases', key: 'purchases' },
  { label: 'Views', key: 'views' },
  { label: ' ', key: 'actions', align: 'right' },
])

const props = defineProps({
  products: {
    type: Array as PropType<Product[]>,
    required: true,
  },
  loading: { type: Boolean, default: false },
  pagination: { type: Object as PropType<Pagination>, required: true },
  sortBy: { type: String as PropType<Sorting['sortBy']>, required: true },
  sortingOrder: { type: String as PropType<Sorting['sortingOrder']>, required: true },
})

const emit = defineEmits<{
  (event: 'edit-product', product: Product): void
  (event: 'delete-product', product: Product): void
  (event: 'update:sortBy', sortBy: Sorting['sortBy']): void
  (event: 'update:sortingOrder', sortingOrder: Sorting['sortingOrder']): void
}>()

const products = toRef(props, 'products')
const sortByVModel = useVModel(props, 'sortBy', emit)
const sortingOrderVModel = useVModel(props, 'sortingOrder', emit)

const totalPages = computed(() => Math.ceil(props.pagination.total / props.pagination.perPage))

const { confirm } = useModal()

  const onProductDelete = async (product: Product) => {
  const agreed = await confirm({
    title: 'Delete product',
    message: `Are you sure you want to delete ${product.name}?`,
    okText: 'Delete',
    cancelText: 'Cancel',
    size: 'small',
    maxWidth: '380px',
  })

  if (agreed) {
    emit('delete-product', product)
  }
}
</script>

<template>
  <VaDataTable v-model:sort-by="sortByVModel"
    v-model:sorting-order="sortingOrderVModel"
    :columns="columns"
    :items="products"
    :loading="$props.loading">
    <template #cell(name)="{ rowData }">
      <div class="flex items-center gap-2 max-w-[230px] ellipsis">
        <VaAvatar :size="small" :src="rowData.imageUrl" />
        {{ rowData.name }}
      </div>
    </template>

    <template #cell(brand)="{ rowData }">
      <div class="max-w-[120px] ellipsis">
        {{ rowData.brand }}
      </div>
    </template>

    <template #cell(quantit)="{ rowData }">
      <div class="max-w-[120px] ellipsis">
        {{ rowData.quantity }}
      </div>
    </template>

    <template #cell(price)="{ rowData }">
      <div class="max-w-[120px] ellipsis">
        {{ rowData.price }}
      </div>
    </template>

    <template #cell(purchases)="{ rowData }">
      <div class="max-w-[120px] ellipsis">
        {{ rowData.purchases }}
      </div>
    </template>

    <template #cell(views)="{ rowData }">
      <div class="max-w-[120px] ellipsis">
        {{ rowData.views }}
      </div>
    </template>

    <template #cell(actions)="{ rowData }">
      <div class="flex gap-2 justify-end">
        <VaButton preset="primary"
          size="small"
          icon="mso-edit"
          aria-label="Edit product"
          @click="$emit('edit-product', rowData as Product)" />
        <VaButton preset="primary"
          size="small"
          icon="mso-delete"
          color="danger"
          aria-label="Delete product"
          @click="onProductDelete(rowData as Product)" />
      </div>
    </template>
  </VaDataTable>

  <div class="flex flex-col-reverse md:flex-row gap-2 justify-between items-center py-2">
    <div>
      <b>{{ $props.pagination.total }} results.</b>
      Results per page
      <VaSelect v-model="$props.pagination.perPage" class="!w-20" :options="[10, 50, 100]" />
    </div>

    <div v-if="totalPages > 1" class="flex">
      <VaButton
        preset="secondary"
        icon="va-arrow-left"
        aria-label="Previous page"
        :disabled="$props.pagination.page === 1"
        @click="$props.pagination.page--"
      />
      <VaButton
        class="mr-2"
        preset="secondary"
        icon="va-arrow-right"
        aria-label="Next page"
        :disabled="$props.pagination.page === totalPages"
        @click="$props.pagination.page++"
      />
      <VaPagination
        v-model="$props.pagination.page"
        buttons-preset="secondary"
        :pages="totalPages"
        :visible-pages="5"
        :boundary-links="false"
        :direction-links="false"
      />
    </div>
  </div>
</template>

<style lang="scss" scoped>
.va-data-table {
  ::v-deep(.va-data-table__table-tr) {
    border-bottom: 1px solid var(--va-background-border);
  }
}
</style>
