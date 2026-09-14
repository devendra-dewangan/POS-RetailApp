export interface NavItem {
    label: string;
    path: string;
    icon: string;
}

/**
 * Global navigation entries shared between the header drawer (mobile menu)
 * and the sidebar. Add a new feature here once its routes are registered
 * in `app-routing-module.ts`.
 */
export const NAV_ITEMS: readonly NavItem[] = [
    { label: 'Dashboard', path: '/dashboard', icon: '@tui.layout-dashboard' },
    { label: 'Purchase', path: '/purchase', icon: '@tui.box' },
    { label: 'Import', path: '/import', icon: '@tui.upload' },
];