// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------


function getElementOrThrow(id) {
  const el = document.getElementById(id);
  if (!el) throw new Error(`Element with ID ${id} not found`);
  return el;
}

function createElement(tag, props, children = []) {
  const el = document.createElement(tag);
  Object.assign(el, props);
  children.forEach(child => el.appendChild(child));
  return el;
}

export function setTreeModel(id, treeModel) {
  try {
    const element = getElementOrThrow(id);
    element.renderItem = (_, item, __, context, update) => {
      const { icon: iconName, name: itemName } = item.data || {};
      const children = [];

      if (iconName) {
        children.push(createElement('ix-icon', {
          name: iconName,
          style: 'margin-right: 0.5rem'
        }));
      }

      let nameEl;
      if (itemName) {
        nameEl = createElement('span', { innerText: itemName });
        children.push(nameEl);
      }

      const el = createElement('ix-tree-item', {
        hasChildren: item.hasChildren,
        context: context[item.id]
      }, children);

      update(updateTreeItem => {
        const uData = updateTreeItem.data || {};
        if (nameEl && uData.name) nameEl.innerText = uData.name;
      });

      return el;
    };
    element.model = JSON.parse(treeModel);
  } catch (error) {
    console.error('Failed to set tree model:', error);
  }
}

export function setTreeContext(id, treeContext) {
  try {
    const element = getElementOrThrow(id);
    element.context = JSON.parse(treeContext);
  } catch (error) {
    console.error('Failed to set tree context:', error);
  }
}

export function markItemAsDirty(treeId, itemIdentifiers) {
  console.log('markItemAsDirty called:', treeId, itemIdentifiers);
  const treeElement = document.getElementById(treeId);
  if (!treeElement) {
    console.warn(`Tree element with id '${treeId}' not found`);
    return;
  }
  if (typeof treeElement.markItemsAsDirty === 'function') {
    treeElement.markItemsAsDirty(itemIdentifiers);
    console.log('Items marked as dirty:', itemIdentifiers);
  } else {
    console.warn('markItemsAsDirty method not available on tree element');
  }
}
