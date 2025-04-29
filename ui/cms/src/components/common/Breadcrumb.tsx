import {
  Breadcrumb as ChakraBreadcrumb,
  Show,
  type SystemStyleObject,
} from "@chakra-ui/react";
import * as React from "react";
import { Link } from "react-router-dom";

export interface BreadcrumbItem {
  title: string;
  path: string;
}
export interface BreadcrumbProps extends ChakraBreadcrumb.RootProps {
  separator?: React.ReactNode;
  separatorGap?: SystemStyleObject["gap"];
  items: BreadcrumbItem[];
}

export const Breadcrumb = React.forwardRef<HTMLDivElement, BreadcrumbProps>(
  function BreadcrumbRoot(props, ref) {
    const { separator, separatorGap, items, ...rest } = props;

    return (
      <ChakraBreadcrumb.Root ref={ref} {...rest}>
        <ChakraBreadcrumb.List gap={separatorGap}>
          {items.map((item, index) => {
            const last = index === items.length - 1;
            return (
              <React.Fragment key={index}>
                <Show when={!last}>
                  <ChakraBreadcrumb.Item>
                    <ChakraBreadcrumb.Link asChild>
                      <Link to={item.path}>{item.title}</Link>
                    </ChakraBreadcrumb.Link>
                  </ChakraBreadcrumb.Item>
                </Show>

                <Show
                  when={last}
                  fallback={
                    <ChakraBreadcrumb.Separator>
                      {separator}
                    </ChakraBreadcrumb.Separator>
                  }
                >
                  <ChakraBreadcrumb.Item>
                    <ChakraBreadcrumb.CurrentLink>
                      {item.title}
                    </ChakraBreadcrumb.CurrentLink>
                  </ChakraBreadcrumb.Item>
                </Show>
              </React.Fragment>
            );
          })}
        </ChakraBreadcrumb.List>
      </ChakraBreadcrumb.Root>
    );
  }
);
