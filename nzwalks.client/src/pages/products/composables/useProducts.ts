import { Ref, ref, unref, watch } from 'vue'
import { getProducts, updateProduct, addProduct, removeProduct, type Filters, Pagination, Sorting } from '../../../data/pages/products'
import { Product } from '../types'
import { watchIgnorable } from '@vueuse/core'

const makePaginationRef = () => ref<Pagination>({ page: 1, perPage: 10, total: 0 })
const makeSortingRef = () => ref<Sorting>({ sortBy: 'name', sortingOrder: null })
const makeFiltersRef = () => ref<Partial<Filters>>({ isActive: true, search: '' })

export const useProducts = (options?: {
  pagination?: Ref<Pagination>
  sorting?: Ref<Sorting>
  filters?: Ref<Partial<Filters>>
}) => {
  const isLoading = ref(false)
  const products = ref<Product[]>([])

  const { filters = makeFiltersRef(), sorting = makeSortingRef(), pagination = makePaginationRef() } = options || {}

  const fetch = async () => {
    isLoading.value = true
    const { data, pagination: newPagination } = await getProducts({
      ...unref(filters),
      ...unref(sorting),
      ...unref(pagination),
    })
    products.value = data

    ignoreUpdates(() => {
      pagination.value = newPagination
    })

    isLoading.value = false
  }

  const { ignoreUpdates } = watchIgnorable([pagination, sorting], fetch, { deep: true })

  watch(
    filters,
    () => {
      // Reset pagination to first page when filters changed
      pagination.value.page = 1
      fetch()
    },
    { deep: true },
  )

  fetch()

  return {
    isLoading,

    filters,
    sorting,
    pagination,

    products,

    fetch,

    async add(product: Product) {
      isLoading.value = true
      await addProduct(product)
      await fetch()
      isLoading.value = false
    },

    async update(product: Product) {
      isLoading.value = true
      await updateProduct(product)
      await fetch()
      isLoading.value = false
    },

    async remove(product: Product) {
      isLoading.value = true
      await removeProduct(product)
      await fetch()
      isLoading.value = false
    },
  }
}
