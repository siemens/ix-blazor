// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

function getElementOrThrow(id) {
  const element = document.getElementById(id);
  if (!element) {
    throw new Error(`Element with ID ${id} not found`);
  }
  return element;
}

function createElement(tag, props, children = []) {
  const element = document.createElement(tag);
  Object.assign(element, props);
  children.forEach((child) => element.appendChild(child));
  return element;
}

export function setTreeModel(id, treeModel) {
  const element = getElementOrThrow(id);
  const model = JSON.parse(treeModel);

  element.renderItem = (_, item, __, context, update) => {
    const { icon: iconName, name: itemName } = item.data || {};
    const children = [];

    if (iconName) {
      children.push(createElement('ix-icon', {
        name: iconName,
        style: 'margin-right: 0.5rem',
      }));
    }

    const itemContext = context[item.id];
    const treeItem = createElement('ix-tree-item', {
      text: itemName,
      hasChildren: item.hasChildren,
      context: itemContext,
      disabled: Boolean(item.disabled || itemContext?.isDisabled),
    }, children);

    update((updatedItem) => {
      treeItem.text = updatedItem.data?.name;
      treeItem.disabled = Boolean(updatedItem.disabled);
    });

    return treeItem;
  };

  element.model = model;
}

export function setTreeContext(id, treeContext) {
  const element = getElementOrThrow(id);
  element.context = JSON.parse(treeContext);
}

export function setToggleOnItemClick(id, value) {
  const element = getElementOrThrow(id);
  element.toggleOnItemClick = value;
}

export function setTreeItemContext(id, context) {
  const element = getElementOrThrow(id);
  element.context = context ? JSON.parse(context) : undefined;
}

export function setTreeItemProperty(id, propertyName, propertyValue) {
  const element = getElementOrThrow(id);
  element[propertyName] = propertyValue;
}

export async function refreshTree(id, options = { force: false }) {
  const element = getElementOrThrow(id);
  if (typeof element.refreshTree === 'function') {
    await element.refreshTree(options);
  }
}

export async function markItemsAsDirty(id, itemIdentifiers) {
  const element = document.getElementById(id);
  if (element && typeof element.markItemsAsDirty === 'function') {
    await element.markItemsAsDirty(itemIdentifiers);
  }
}
