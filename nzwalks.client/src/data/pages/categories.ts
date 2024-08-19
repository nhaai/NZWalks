import { sleep } from '../../services/utils'
import { Category } from './../../pages/categories/types'

export let categories = [] as Category[]

// Simulate API calls

export type Pagination = {
  page: number
  perPage: number
  total: number
}

export type Sorting = {
  sortBy: keyof Category | undefined
  sortingOrder: 'asc' | 'desc' | null
}

export type Filters = {
  isActive: boolean
  search: string
}

const getSortItem = (obj: any, sortBy: string) => {
  return obj[sortBy]
}

export const getCategories = async (filters: Partial<Filters & Pagination & Sorting>) => {
  await sleep(1000)
  const { isActive, search, sortBy, sortingOrder } = filters
  const response = await fetch('api/Categories?pageNumber=' + filters.page + '&pageSize=' + filters.perPage)
  const metadata = JSON.parse((await response.headers.get('x-pagination')) || '{}')
  let filteredCategories = (await response.json()) as Category[]

  filteredCategories = filteredCategories.filter((category) => category.active === isActive)

  if (search) {
    filteredCategories = filteredCategories.filter((category) => category.name.toLowerCase().includes(search.toLowerCase()))
  }

  if (sortBy && sortingOrder) {
    filteredCategories = filteredCategories.sort((a, b) => {
      const first = getSortItem(a, sortBy)
      const second = getSortItem(b, sortBy)
      if (first > second) {
        return sortingOrder === 'asc' ? 1 : -1
      }
      if (first < second) {
        return sortingOrder === 'asc' ? -1 : 1
      }
      return 0
    })
  }

  const { page = 1, perPage = 10 } = filters || {}
  return {
    data: (categories = filteredCategories),
    pagination: {
      page,
      perPage,
      total: metadata?.TotalItemCount || filteredCategories.length,
    },
  }
}

export const addCategory = async (category: Category) => {
  await sleep(1000)
  const requestOptions = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(category),
  }
  const response = await fetch('api/Categories', requestOptions)
  const newCategory = (await response.json()) as Category
  categories.unshift(newCategory)
}

export const updateCategory = async (category: Category) => {
  await sleep(1000)
  const requestOptions = {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(category),
  }
  const response = await fetch('api/Categories/' + category.id, requestOptions)
  const existingCategory = (await response.json()) as Category
  const index = categories.findIndex((x) => x.id === category.id)
  categories[index] = existingCategory
}

export const removeCategory = async (category: Category) => {
  await sleep(1000)
  await fetch('api/Categories/' + category.id, { method: 'DELETE' })
  categories.splice(
    categories.findIndex((x) => x.id === category.id),
    1,
  )
}
